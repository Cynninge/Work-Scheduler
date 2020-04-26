//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using WorkScheduler.Context;
//using WorkScheduler.Models;
//using WorkScheduler.Services.Interfaces;

//namespace WorkScheduler.Services
//{
//    public class DayService : IDayService
//    {
//        private readonly int currentDateInt = DateTime.Now.DayOfYear;
        
//        private readonly EFCContext _context;
//        private readonly DayModel _dayContext;
//        public DayService(EFCContext context)
//        {
//            _context = context;
//        }
        
//        public void Create([Bind("Id,UserId,Hour,DayName,IsItHoliday,Date")] DayModel dayModel, IdentityUser user, DateTime dateIncrementation)
//        { 
//                dayModel.DayName = dateIncrementation.DayOfWeek.ToString();
//                dayModel.Date = dateIncrementation;                
//                dayModel.User = user;
//                if (dayModel.DayName == "Saturday" || dayModel.DayName == "Sunday")
//                {
//                    dayModel.IsItHoliday = true;
//                }                                
//                _context.Add(dayModel);
//                _context.SaveChanges();
                
            
            
//            //var DaysListOfUser = new List<DayModel>();
//            //for (int i = currentDateInt; i <= 365; i++)
//            //{
//            //    var DayA = new DayModel();
//            //    DayA.Date = currentDate.AddDays(1);
//            //    DayA.DayName = currentDate.DayOfWeek.ToString();
//            //    DaysListOfUser.Add(DayA);
//            //}
//            //return DaysListOfUser;
//        }

//        public DayModel Get(int id)
//        {
//            throw new NotImplementedException();
//        }

//        public IList<DayModel> GetAll()
//        {
//            throw new NotImplementedException();
//        }

//        public IList<DayModel> GetCurrentMonth(DateTime start, DateTime end)
//        {
//            throw new NotImplementedException();
//        }

//        public IList<DayModel> GetCurrentWeek(DateTime start, DateTime end)
//        {
//            throw new NotImplementedException();
//        }

//        public IList<DayModel> GetDaysByDate(DateTime start, DateTime end)
//        {
//            throw new NotImplementedException();
//        }

//        public bool Update(DayModel day)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
