using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursosEntity.Model;

namespace CursosEntity
{
   public class Ejercicio
    {
       private static void InitDb()
       {
           using (var db=new cursosEntities())
           {
                if (db.Aula.Any())
                    return;


                var la=new List<Aula>()
                {
                    new Aula() {capacidad = 22,nombre = "Aristoteles"},
                    new Aula() {capacidad = 15,nombre = "Platon"},
                    new Aula() {capacidad = 10,nombre = "Descartes"},
                    new Aula() {capacidad = 16,nombre = "Galileo"},

                };

              db.Aula.AddRange(la);
                var lc=new List<Curso>()
                {
                    new Curso() {Aula = la.First(o=>o.nombre=="Descartes"),duracion = 120,inicio = DateTime.Now,
                        fin = DateTime.Now.AddDays(30),nombre = "C++"},
                    new Curso() {Aula = la[0],duracion = 5,inicio = DateTime.Now,
                        fin = DateTime.Now.AddDays(1),nombre = "Inteligencia emocional"},
                };
              
               db.Curso.AddRange(lc);

               var lp=new List<Profesor>()
               {
                   new Profesor() {edad = 29,nombre = "Pepe perez"},
                   new Profesor() {edad = 33,nombre = "Eva Jimenez"},
               };

               db.Profesor.AddRange(lp);

                var lal=new List<Alumno>()
                {
                    new Alumno() {dni="12345678A",nombre="Manolo Gomez" },
                    new Alumno() {dni="98765432B",nombre="Miguel Perez" },
                    new Alumno() {dni="12349876Z",nombre="Maria Gutierrez" },
                };
                lal[0].Curso.Add(lc[1]);
                lal[0].Curso.Add(lc[0]);
                lal[1].Curso.Add(lc[1]);
                lal[2].Curso.Add(lc[1]);
                lal[2].Curso.Add(lc[0]);

               db.Alumno.AddRange(lal);

               var lpc = new List<ProfesorCurso>()
               {
                   
                   new ProfesorCurso() {Curso = lc[1],Profesor = lp[0],horas = 100},
                   new ProfesorCurso() {Curso = lc[0],Profesor = lp[0],horas = 100},
                   new ProfesorCurso() {Curso = lc[1],Profesor = lp[1],horas = 100},
                   new ProfesorCurso() {Curso = lc[0],Profesor = lp[1],horas = 100},
               };
               db.ProfesorCurso.AddRange(lpc);

               try
               {
                   db.SaveChanges();
               }
               catch (Exception e)
               {
                   Console.WriteLine(e);
               }



           }


       }

       private static void ListadoCursos()
       {
           using (var db=new cursosEntities())
           {
                foreach (var item in db.Curso)
                {
                    Console.WriteLine(item);
                }
            }
           
       }

        private static void CursosProfesor()
        {
            using (var db = new cursosEntities())
            {
                Console.WriteLine("Profesor:");
                var texto = Console.ReadLine();
                var profe = db.Profesor.FirstOrDefault(o => o.nombre == texto);

                if (profe == null)
                {
                    Console.WriteLine("Profesor no encontrado");
                    return;
                }

                var cursos = db.ProfesorCurso.Where(o => o.idProfesor == profe.idProfesor).Select(o=>o.Curso);

                foreach (var profesorCurso in cursos)
                {
                    Console.WriteLine(profesorCurso);
                }



            }

        }
        private static void HorasProfesor()
        {
            using (var db = new cursosEntities())
            {
                Console.WriteLine("Profesor:");
                var texto = Console.ReadLine();
                var profe = db.Profesor.FirstOrDefault(o => o.nombre == texto);

                if (profe == null)
                {
                    Console.WriteLine("Profesor no encontrado");
                    return;
                }

                var horas = db.ProfesorCurso.Where(o => o.idProfesor == profe.idProfesor).Sum(o => o.horas);


                    Console.WriteLine("Horas totales {0}",horas);




            }

        }
        private static void AlumnosCurso()
        {
            using (var db = new cursosEntities())
            {
                Console.WriteLine("Curso:");
                var texto = Console.ReadLine();
                var curso = db.Curso.FirstOrDefault(o => o.nombre == texto);

                if (curso == null)
                {
                    Console.WriteLine("Curso no encontrado");
                    return;
                }

                foreach (var alumno in curso.Alumno)
                {
                    Console.WriteLine(alumno);
                }
            }

        }
        private static void ProfesoresAlumno()
        {
            using (var db = new cursosEntities())
            {
                Console.WriteLine("Alumno:");
                var texto = Console.ReadLine();
                var alumno = db.Alumno.FirstOrDefault(o => o.nombre == texto);

                if (alumno == null)
                {
                    Console.WriteLine("Alumno no encontrado");
                    return;
                }

                var idc = alumno.Curso.Select(o => o.idCurso);

                var pc = db.ProfesorCurso.Where(o => idc.Contains(o.idCurso)).
                    Select(o => o.Profesor).Distinct();

                foreach (var profesor in pc)
                {
                    Console.WriteLine(profesor);
                }

            }

        }
        public static void Main(string[] args)
        {

            InitDb();

            int r = 0;

            do
            {
                Console.WriteLine("1. Listar cursos");
                Console.WriteLine("2. Cursos profesor");
                Console.WriteLine("3. Horas profesor");
                Console.WriteLine("4. Alumnos curso");
                Console.WriteLine("5. Profesores alumno");
                Console.WriteLine("6. Salir");
                Console.Write("Opcion:");
                r = Convert.ToInt32(Console.ReadLine());

                switch (r)
                {
                    case 1:
                        ListadoCursos();
                        break;
                    case 2:
                        CursosProfesor();
                        break;
                    case 3:
                        HorasProfesor();
                        break;
                    case 4:
                        AlumnosCurso();
                        break;
                    case 5:
                        ProfesoresAlumno();
                        break;
                }

            } while (r != 6);







        }
    }
}
