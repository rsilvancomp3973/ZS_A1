namespace ZenithWebSite.Migrations.ZenithMigrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using ZenithDataLib.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ZenithDataLib.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\ZenithMigrations";
        }

        protected override void Seed(ZenithDataLib.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            // Create an ADMIN role
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            // Create a MEMBER role
            if (!context.Roles.Any(r => r.Name == "Member"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Member" };

                manager.Create(role);
            }


            // Create an admin user named 'a'
            if (!context.Users.Any(u => u.UserName == "a"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "a", Email = "a@a.a" };

                manager.Create(user, "P@$$w0rd");
                manager.AddToRole(user.Id, "Admin");
            }

            // Create a member user named 'm'
            if (!context.Users.Any(u => u.UserName == "m"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "m", Email = "m@m.m" };

                manager.Create(user, "P@$$w0rd");
                manager.AddToRole(user.Id, "Member");
            }

            if (!context.Activities.Any())
            {
                context.Activities.AddOrUpdate(getActivities().ToArray());
                context.SaveChanges();
            }

            if (!context.Events.Any())
            {
                context.Events.AddOrUpdate(e => e.EventId, getEvents(context).ToArray());
                context.SaveChanges();
            }
        }

        private List<ActivityType> getActivities()
        {
            List<ActivityType> activities = new List<ActivityType>();
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Senior's Golf Tournament",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Leadership General Assembly Meeting",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Youth Bowling Tournament",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Young Ladies Cooking Lessons",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Youth Craft Lessons",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Youth Choir Practice",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Lunch",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Pancake Breakfast",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Swimming Lessons for the Youth",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Swimming Exercise for Parents",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Bingo Tournament",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "BBQ Lunch",
                CreationDate = DateTime.Now
            });
            activities.Add(new ActivityType()
            {
                ActivityDescription = "Garage Sale",
                CreationDate = DateTime.Now
            });
            return activities;
        }

        private List<Event> getEvents(ApplicationDbContext db)
        {
            List<Event> events = new List<Event>();
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 17, 08, 30, 00),
                EventTo = new DateTime(2017, 10, 17, 10, 30, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Senior's Golf Tournament"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 18, 08, 30, 00),
                EventTo = new DateTime(2017, 10, 18, 10, 30, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Leadership General Assembly Meeting"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 20, 17, 30, 00),
                EventTo = new DateTime(2017, 10, 20, 19, 15, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Youth Bowling Tournament"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 20, 19, 00, 00),
                EventTo = new DateTime(2017, 10, 20, 20, 00, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Young Ladies Cooking Lessons"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 21, 08, 30, 00),
                EventTo = new DateTime(2017, 10, 21, 10, 30, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Youth Craft Lessons"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 21, 10, 30, 00),
                EventTo = new DateTime(2017, 10, 21, 12, 00, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Youth Choir Practice"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 21, 12, 00, 00),
                EventTo = new DateTime(2017, 10, 21, 01, 30, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Lunch"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 22, 07, 30, 00),
                EventTo = new DateTime(2017, 10, 22, 08, 30, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Pancake Breakfast"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 22, 08, 30, 00),
                EventTo = new DateTime(2017, 10, 22, 10, 30, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Swimming Lessons for the Youth"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 22, 08, 30, 00),
                EventTo = new DateTime(2017, 10, 22, 10, 30, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Swimming Exercise for Parents"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 22, 10, 30, 00),
                EventTo = new DateTime(2017, 10, 22, 12, 00, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Bingo Tournament"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 22, 12, 00, 00),
                EventTo = new DateTime(2017, 10, 22, 13, 00, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "BBQ Lunch"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });
            events.Add(new Event()
            {
                EventFrom = new DateTime(2017, 10, 22, 13, 00, 00),
                EventTo = new DateTime(2017, 10, 22, 18, 00, 00),
                ActivityType = db.Activities.First(a => a.ActivityDescription == "Garage Sale"),
                EnteredBy = db.Users.First(a => a.UserName == "a"),
                CreationDate = DateTime.Now,
                isActive = true
            });

            return events;
        }
    }
}
