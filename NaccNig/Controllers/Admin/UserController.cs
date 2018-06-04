
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using NaccNig;
using NaccNig.Models;
using NaccNig.ViewModels;
using NaccNigModels.Structures;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using NaccNigModels.PopUp;
using NaccNigModels.Members;

namespace NaccNig.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private NaccNigDbContext db = new NaccNigDbContext();
        //
        // GET: User/List
        public ActionResult List()
        {
            using (var database = new NaccNigDbContext())
            {
                var users = database.Users.ToList();
                var member = database.ActiveMember.ToList();

                var admins = GetAdminUserNames(users, member, database);
                ViewBag.Admins = admins;

                return View(users);
            }
        }
        //
        // GET: User
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        private HashSet<string> GetAdminUserNames(List<ApplicationUser>users, List<ActiveMember> member, NaccNigDbContext conext)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(conext));

            var admins = new HashSet<string>();

            foreach (var user in users)
            {
                if (userManager.IsInRole(user.Id, "Admin"))
                {
                    admins.Add(user.UserName);
                }
            }

            return admins;
        }

        //
        // GET: User/Edit
        public ActionResult Update(string id)
        {

            //Validate Id
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var database = new NaccNigDbContext())
            {
                if (User.IsInRole(RoleName.Admin))
                {
                    var user = database.Users
                   .Where(u => u.Id == id)
                   .First();

                    if (user == null)
                    {
                        return HttpNotFound();
                    }
                    var viewModel = new EditUserViewModel();
                    viewModel.User = user;
                    viewModel.Roles = GetUserRoles(user, database);


                    return View(viewModel);
                }
                if (User.IsInRole(RoleName.ActiveMember))
                {

                    var member = database.ActiveMember.Where(x => x.ActiveMemberId == id).First();

                    if (member == null)
                    {
                        return HttpNotFound();
                    }

                    var viewModel = new EditUserViewModel();

                    viewModel.Address = member.Address;
                    viewModel.Age = member.Age;
                    viewModel.CallUpNumber = member.CallUpNumber;
                    viewModel.Dob = member.Dob;
                    viewModel.Firstname = member.Firstname;
                    viewModel.Gender = member.Gender;
                    viewModel.Middlename = member.Middlename;
                    viewModel.PhoneNumber = member.PhoneNumber;
                    viewModel.Photo = member.Photo;
                    viewModel.Position = member.Position;
                    viewModel.StateCode = member.StateCode;
                    viewModel.Lastname = member.Surname;


                    viewModel.Roles = GetMemberRoles(member, database);

                    var myGender = from Gender s in Enum.GetValues(typeof(Gender))
                                   select new { ID = s, Name = s.ToString() };
                    var myState = from State s in Enum.GetValues(typeof(State))
                                  select new { ID = s, Name = s.ToString() };
                    var depState = from StateDeployed s in Enum.GetValues(typeof(StateDeployed))
                                   select new { ID = s, Name = s.ToString() };
                    ViewBag.Gender = new SelectList(myGender, "Name", "Name");
                    ViewBag.StateOfOrigin = new SelectList(myState, "Name", "Name");
                    ViewBag.StateOfDeployment = new SelectList(depState, "Name", "Name");
                    ViewBag.StateList = new SelectList(StateChapter.GetStateChapters(), "StateChapId", "StateChapterName");
                    ViewBag.Zone = new SelectList(Zone.GetZones(), "ZId", "ZoneName");

                    return View(viewModel);
                }
                return View();
                }
        }
           


        //
        //POST : User/Edit
        [HttpPost]
        public ActionResult Update(string id, EditUserViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                using (var database = new NaccNigDbContext())
                {
                    var user = database.Users.FirstOrDefault(u => u.Id == id);

                    if (user == null)
                    {
                        return HttpNotFound();
                    }

                    if (!string.IsNullOrEmpty(viewModel.Password))
                    {
                        var hasher = new PasswordHasher();
                        var passwordHash = hasher.HashPassword(viewModel.Password);
                        user.PasswordHash = passwordHash;
                    }


                    user.Email = viewModel.User.Email;
                    
                    this.SetUserRoles(viewModel, user, database);


                    database.Entry(user).State = EntityState.Modified;
                    database.SaveChanges();

                    
                    return RedirectToAction("List");
                }
            }
            var myGender = from Gender s in Enum.GetValues(typeof(Gender))
                           select new { ID = s, Name = s.ToString() };
            var myState = from State s in Enum.GetValues(typeof(State))
                          select new { ID = s, Name = s.ToString() };
            var depState = from StateDeployed s in Enum.GetValues(typeof(StateDeployed))
                           select new { ID = s, Name = s.ToString() };
            ViewBag.Gender = new SelectList(myGender, "Name", "Name");
            ViewBag.StateOfOrigin = new SelectList(myState, "Name", "Name");
            ViewBag.StateOfDeployment = new SelectList(depState, "Name", "Name");
            ViewBag.StateList = new SelectList(StateChapter.GetStateChapters(), "StateChapId", "StateChapterName");
            ViewBag.Zone = new SelectList(Zone.GetZones(), "ZId", "ZoneName");
            return View(viewModel);
        }

        //
        //GET: User/Delete
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using( var database = new NaccNigDbContext())
            {
                var user = database.Users
                    .Where(u => u.Id.Equals(id))
                    .First();

                if (user == null)
                {
                    return HttpNotFound();
                }


                return View(user);
            }
        }

        //
        //POST: User/Delete
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            using (var database = new NaccNigDbContext())
            {
                var user = database.Users
                    .Where(u => u.Id.Equals(id))
                    .First();

                var userArticles = database.Post
                    .Where(a => a.UserId == user.Id);

                foreach(var article in userArticles)
                {
                    database.Post.Remove(article);
                }

                database.Users.Remove(user);
                database.SaveChanges();


                return RedirectToAction("List");
            }
        }

        private void SetUserRoles(EditUserViewModel model, ApplicationUser user, NaccNigDbContext database)
        {
            var userManager = Request
                .GetOwinContext()
                .GetUserManager<ApplicationUserManager>();

            foreach (var role in model.Roles)
            {
                if (role.IsSelected)
                {
                    userManager.AddToRole(user.Id, role.Name);
                }
                else if (!role.IsSelected)
                {
                    userManager.RemoveFromRole(user.Id, role.Name);
                }
            }
        }

        private IList<Role> GetUserRoles(ApplicationUser user, NaccNigDbContext database)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var roles = database.Roles
                .Select(r => r.Name)
                .OrderBy(r => r)
                .ToList();

            var userRoles = new List<Role>();

            foreach (var roleName in roles)
            {
                var role = new Role { Name = roleName };

                if (userManager.IsInRole(user.Id, roleName))
                {
                    role.IsSelected = true;
                }

                userRoles.Add(role);
            }

            return userRoles;
        }

        private IList<Role> GetMemberRoles(ActiveMember member, NaccNigDbContext database)
        {
            var userManager = Request.GetOwinContext().GetUserManager<ApplicationUserManager>();

            var roles = database.Roles
                .Select(r => r.Name)
                .OrderBy(r => r)
                .ToList();

            var userRoles = new List<Role>();

            foreach (var roleName in roles)
            {
                var role = new Role { Name = roleName };

                if (userManager.IsInRole(member.ActiveMemberId, roleName))
                {
                    role.IsSelected = true;
                }

                userRoles.Add(role);
            }

            return userRoles;
        }


    }
}