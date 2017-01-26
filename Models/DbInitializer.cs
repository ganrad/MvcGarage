using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcGarage.Data;

namespace MvcGarage.Models
{
    public static class DbInitializer
    {
        public static void Initialize(RepairContext context)
        {
            context.Database.EnsureCreated();

            // Look for any existing repairs.
            if (context.Repairs.Any())
            {
                return;   // DB has been seeded
            }

            var repairs = new Repair[]
            {
            new Repair{Vehicle="Subaru Impreza",RepairDate=DateTime.Parse("01-16-2017"),RepairType="Oil Change",Miles=83015,Workshop="Home",Workdone="Used Mobil 1 oil and filter."},
            new Repair{Vehicle="Subaru Impreza",RepairDate=DateTime.Parse("07-12-2016"),RepairType="Oil Change",Miles=74815,Workshop="Home",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="Subaru Impreza",RepairDate=DateTime.Parse("01-11-2016"),RepairType="Oil Change",Miles=68600,Workshop="Walmart, Lynwood",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="VW GTI 1.8T",RepairDate=DateTime.Parse("04-12-2009"),RepairType="Oil Change",Miles=72431,Workshop="Pepboys",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="Subaru Impreza",RepairDate=DateTime.Parse("07-28-2015"),RepairType="Oil Change",Miles=62717,Workshop="Home",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="VW GTI 1.8T",RepairDate=DateTime.Parse("11-19-2008"),RepairType="Oil Change",Miles=65420,Workshop="Pepboys",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="VW GTI 1.8T",RepairDate=DateTime.Parse("10-29-2007"),RepairType="Oil Change",Miles=59528,Workshop="Pepboys",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="VW GTI 1.8T",RepairDate=DateTime.Parse("04-06-2007"),RepairType="Oil Change",Miles=51323,Workshop="Pepboys",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="VW GTI 1.8T",RepairDate=DateTime.Parse("11-21-2006"),RepairType="Oil Change",Miles=44111,Workshop="Pepboys",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="VW GTI 1.8T",RepairDate=DateTime.Parse("05-12-2006"),RepairType="Oil Change",Miles=39220,Workshop="Pepboys",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="Subaru Impreza",RepairDate=DateTime.Parse("01-19-2015"),RepairType="Oil Change",Miles=56319,Workshop="Walmart",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="Subaru Impreza",RepairDate=DateTime.Parse("10-23-2014"),RepairType="Oil Change",Miles=51624,Workshop="Home",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="Subaru Impreza",RepairDate=DateTime.Parse("04-05-2014"),RepairType="Oil Change",Miles=45261,Workshop="Home",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="Subaru Impreza",RepairDate=DateTime.Parse("10-05-2013"),RepairType="Oil Change",Miles=63990,Workshop="Subaru dealership",Workdone="Used Mobil 1 extended perf. oil and filter."},
            new Repair{Vehicle="Toyota RAV4 V6 3.5L",RepairDate=DateTime.Parse("10-28-2012"),RepairType="Oil Change",Miles=102864,Workshop="Toyota Dealership, Kirkland",Workdone="Used Pennzoil Oil and Fram oil filter."},
            new Repair{Vehicle="Honda Civic 1.7L",RepairDate=DateTime.Parse("04-02-2006"),RepairType="Oil Change",Miles=36441,Workshop="Honda Dealership, Roseville",Workdone="Castrol 5W-30 oil and Fram oil filter."}
            };
            foreach (Repair r in repairs)
            {
                context.Repairs.Add(r);
            }
            context.SaveChanges();
        }
    }
}