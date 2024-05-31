using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Exercise6.Models;

namespace Exercise6
{
    public static class LinqTasks
    {
        public static IEnumerable<Emp> Emps { get; set; }
        public static IEnumerable<Dept> Depts { get; set; }

        static LinqTasks()
        {
            #region Load depts

            List<Dept> depts =
            [
                new Dept
                {
                    Deptno = 1,
                    Dname = "Research",
                    Loc = "Warsaw"
                },
                new Dept
                {
                    Deptno = 2,
                    Dname = "Human Resources",
                    Loc = "New York"
                },
                new Dept
                {
                    Deptno = 3,
                    Dname = "IT",
                    Loc = "Los Angeles"
                }
            ];

            Depts = depts;

            #endregion

            #region Load emps

            var e1 = new Emp
            {
                Deptno = 1,
                Empno = 1,
                Ename = "Jan Kowalski",
                HireDate = DateTime.Now.AddMonths(-5),
                Job = "Backend programmer",
                Mgr = null,
                Salary = 2000
            };

            var e2 = new Emp
            {
                Deptno = 1,
                Empno = 20,
                Ename = "Anna Malewska",
                HireDate = DateTime.Now.AddMonths(-7),
                Job = "Frontend programmer",
                Mgr = e1,
                Salary = 4000
            };

            var e3 = new Emp
            {
                Deptno = 1,
                Empno = 2,
                Ename = "Marcin Korewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Frontend programmer",
                Mgr = null,
                Salary = 5000
            };

            var e4 = new Emp
            {
                Deptno = 2,
                Empno = 3,
                Ename = "Paweł Latowski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Frontend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e5 = new Emp
            {
                Deptno = 2,
                Empno = 4,
                Ename = "Michał Kowalski",
                HireDate = DateTime.Now.AddMonths(-2),
                Job = "Backend programmer",
                Mgr = e2,
                Salary = 5500
            };

            var e6 = new Emp
            {
                Deptno = 2,
                Empno = 5,
                Ename = "Katarzyna Malewska",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Manager",
                Mgr = null,
                Salary = 8000
            };

            var e7 = new Emp
            {
                Deptno = null,
                Empno = 6,
                Ename = "Andrzej Kwiatkowski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "System administrator",
                Mgr = null,
                Salary = 7500
            };

            var e8 = new Emp
            {
                Deptno = 2,
                Empno = 7,
                Ename = "Marcin Polewski",
                HireDate = DateTime.Now.AddMonths(-3),
                Job = "Mobile developer",
                Mgr = null,
                Salary = 4000
            };

            var e9 = new Emp
            {
                Deptno = 2,
                Empno = 8,
                Ename = "Władysław Torzewski",
                HireDate = DateTime.Now.AddMonths(-9),
                Job = "CTO",
                Mgr = null,
                Salary = 12000
            };

            var e10 = new Emp
            {
                Deptno = 2,
                Empno = 9,
                Ename = "Andrzej Dalewski",
                HireDate = DateTime.Now.AddMonths(-4),
                Job = "Database administrator",
                Mgr = null,
                Salary = 9000
            };

            List<Emp> emps =
            [
                e1, e2, e3, e4, e5, e6, e7, e8, e9, e10
            ];

            Emps = emps;

            #endregion
        }

        /// <summary>Backend programmer"
        ///     SELECT * FROM Emps WHERE Job = "Backend programmer";
        /// </summary>
        public static IEnumerable<Emp> Task1()
        {
            var query = from e in Emps where e.Job.Equals("Backend programmer") select e;
            IEnumerable<Emp> result = query;
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps Job = "Frontend programmer" AND Salary>1000 ORDER BY Ename DESC;
        /// </summary>
        public static IEnumerable<Emp> Task2()
        {
            
            var querySyntax =
                from e in Emps
                where e.Job.Equals("Frontend programmer") && e.Salary > 1000
                orderby e.Ename descending
                select e;
            IEnumerable<Emp> result = querySyntax;           
            return result;
            
        }


        /// <summary>
        ///     SELECT MAX(Salary) FROM Emps;
        /// </summary>
        public static int Task3()
        {
            var maxSalary = (from e in Emps select e.Salary).Max();
            int result = maxSalary;
            return result;
        }

        /// <summary>
        ///     SELECT * FROM Emps WHERE Salary=(SELECT MAX(Salary) FROM Emps);
        /// </summary>
        public static IEnumerable<Emp> Task4()
        {
            IEnumerable<Emp> result = null;
            result = from e in Emps where e.Salary == (from emp in Emps select emp.Salary).Max() select e;
            return result;
        }

        /// <summary>
        ///    SELECT ename AS Nazwisko, job AS Praca FROM Emps;
        /// </summary>
        public static IEnumerable<object> Task5()
        {
            var querySyntax =
                from e in Emps 
                select new { Nazwisko = e.Ename, Praca = e.Job };
            IEnumerable<object> result = querySyntax;
            return result;;
        }

        /// <summary>
        ///     SELECT Emps.Ename, Emps.Job, Depts.Dname FROM Emps
        ///     INNER JOIN Depts ON Emps.Deptno=Depts.Deptno
        ///     The result: merge of Emps and Depts collections.
        /// </summary>
        public static IEnumerable<object> Task6()
        {
            IEnumerable<object> result = null;
            var info = from e in Emps
                join d in Depts on e.Deptno equals d.Deptno
                select new { e.Ename, e.Job, d.Dname };
            result = info;
            return result;
        }

        /// <summary>
        ///     SELECT Job AS Praca, COUNT(1) LiczbaPracownikow FROM Emps GROUP BY Job;
        /// </summary>
        public static IEnumerable<object> Task7()
        {
            IEnumerable<object> result = from e in Emps
                group e by e.Job
                into g
                select new { Praca = g.Key, LiczbaPracownikow = g.Count() };
            
            return result;
        }

        /// <summary>
        ///     Return the value "true" if at least one
        ///     of the elements in the collection works as a "Backend programmer".
        /// </summary>
        public static bool Task8()
        {
            bool result = false;
            result = (from e in Emps where e.Job.Equals("Backend programmer") select e).Any();
            return result;
        }

        /// <summary>
        ///     SELECT TOP 1 * FROM Emp WHERE Job="Frontend programmer"
        ///     ORDER BY HireDate DESC;
        /// </summary>
        public static Emp Task9()
        {
            Emp result = null;
            result = (from emp in Emps
                where emp.Job.Equals("Frontend programmer")
                orderby emp.HireDate descending
               select emp).FirstOrDefault();
                
            return result;
        }

        /// <summary>
        ///     SELECT Ename, Job, Hiredate FROM Emps
        ///     UNION
        ///     SELECT "Brak wartości", null, null;
        /// </summary>
        public static IEnumerable<object> Task10()
        {
            
            IEnumerable<object> result = null;
            var first = from e in Emps
                select new { e.Ename, e.Job, e.HireDate };

            result = first.Union(
                new[] {
                new { Ename = "Brak wartości", Job = (string)null, HireDate = (DateTime?)null }
                      });

            return result;
        }

        /// <summary>
        /// Using LINQ, retrieve employees divided by departments, keeping in mind that:
        /// 1. We are only interested in departments with more than 1 employee
        /// 2. We want to return a list of objects with the following structure:
        ///    [
        ///      {name: "RESEARCH", numOfEmployees: 3},
        ///      {name: "SALES", numOfEmployees: 5},
        ///      ...
        ///    ]
        /// 3. Use anonymous types
        /// </summary>
        public static IEnumerable<object> Task11()
        {
            IEnumerable<object> result = null;
             result = from d in Depts
                join e in Emps on
                    d.Deptno equals e.Deptno
                group d by d.Dname
                into g
                where g.Count()>0
                select new { name = g.Key, numOfEmployees =g.Count() };
            
            
            return result;
        }

        /// <summary>
        /// Write your own extension method that will allow the following code snippet to compile.
        /// Add the method to the CustomExtensionMethods class, which is defined below.
        ///
        /// The method should return only those employees who have at least 1 direct subordinate.
        /// Employees should be sorted within the collection by surname (ascending) and salary (descending).
        /// </summary>
        public static IEnumerable<Emp> Task12()
        {
            IEnumerable<Emp> result = Emps.moreThatOneSubordinates();
            return result;
        }

        /// <summary>
        /// The method below should return a single int number.
        /// It takes a list of integers as input.
        /// Try to find, using LINQ, the number that appears an odd number of times in the array of ints.
        /// It is assumed that there will always be one such number.
        /// For example: {1,1,1,1,1,1,10,1,1,1,1} => 10
        /// </summary>
        public static int Task13(int[] arr)
        {
            int result = 0;
            result= (from g in (from num in arr
                group num by num into g
                select g)
                where g.Count() % 2 != 0  select g).First().Key;
            return result;
        }

        /// <summary>
        /// Return only those departments that have exactly 5 employees or no employees at all.
        /// Sort the result by department name in ascending order.
        /// </summary>
        public static IEnumerable<Dept> Task14()
        {
            IEnumerable<Dept> result = null;
            result = from dep in Depts
                where (from e in Emps
                    group e.Deptno by e.Deptno into g
                    where g.Count() == 5 
                    select g.Key).Contains(dep.Deptno) 
                      || !(from em in Emps select em.Deptno).Contains(dep.Deptno)
                select dep;
                
                
            return result;
        }
    }

    public static class CustomExtensionMethods
    {
        public static IEnumerable<Emp> moreThatOneSubordinates(this IEnumerable<Emp> emp)
        {
            var result = from e in emp where 
                (from em in emp select em.Mgr).Contains(e)
                orderby e.Ename ascending, e.Salary descending 
                select e;
            return result;
        }

    }
}