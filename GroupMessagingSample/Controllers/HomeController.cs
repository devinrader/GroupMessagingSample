using GroupMessagingSample.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio;
using Twilio.TwiML;
using Twilio.TwiML.Mvc;

namespace GroupMessagingSample.Controllers
{
    public class HomeController : TwilioController
    {
        GroupSmsDataContext ctx = new GroupSmsDataContext();

        //
        // GET: /IncomingMessage/
        public ActionResult IncomingMessage(string From, string To, string Body)
        {
            var response = new TwilioResponse();
            
            switch (Body.ToLowerInvariant().Trim()) 
            {
                case "start":
                case "join":
                    // subscribe member
                    AddMemberToGroup(From);
                    response.Message("SYSTEM: Welcome to the club :) Keep those messages under 140 characters! Send STOP to leave.");
                    break;
                case "stop":
                case "leave":
                    // unsubscribe member
                    RemoveMemberFromGroup(From);
                    response.Message("SYSTEM: See ya later alligator.");
                    break;
                default:
                    // prevent non-members from sending messages to the group
                    var member = ctx.Members.FirstOrDefault(m => m.PhoneNumber == From);
                    if (member == null)
                    {
                        response.Message("SYSTEM: Send START to send/receive messages from this group");
                        break;
                    }

                    // set up the Twilio client
                    var client = new TwilioRestClient(Credentials.AccountSid, Credentials.AuthToken);

                    // grab all the members of the group, except the sender
                    var members = ctx.Members.Where(m => m.MemberId != member.MemberId).ToList();
                    foreach (var recipient in members) 
                    {
                        var result = client.SendMessage(
                            recipient.PhoneNumber,
                            To,
                            From + ": " + Body.Trim()
                        );

                    }

                    response.Message(string.Format("SYSTEM: Message sent to {0} members", members.Count));
                    break;
            }

            return TwiML(response);
        }

        private void RemoveMemberFromGroup(string phoneNumber)
        {
            // retrieve member for this phone number
            var member = ctx.Members.FirstOrDefault(m => m.PhoneNumber == phoneNumber);

            // if they exist, delete them from the database
            if (member != null)
            {
                ctx.Members.Remove(member);
                ctx.SaveChanges();
            }
        }

        private void AddMemberToGroup(string phoneNumber)
        {
            // retrieve member for this phone number
            var member = ctx.Members.FirstOrDefault(m => m.PhoneNumber == phoneNumber);

            // if they're already a member, just ignore
            if (member != null) return;

            // add new member to database
            member = new Member();
            member.PhoneNumber = phoneNumber;
            ctx.Members.Add(member);
            ctx.SaveChanges();
        }
	}
}