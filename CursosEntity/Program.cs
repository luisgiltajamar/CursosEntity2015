﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CursosEntity.Model;

namespace CursosEntity
{
    class Program
    {
        static void Mai(string[] args)
        {

           
        }

        public static void ConsultaSimple()
        {
            using(var ctx=new cursosEntities())
            {
                var data = ctx.Profesor.Where(o => o.nombre.Contains("Luis"));

                foreach (var profesor in data)
                {
                    Console.WriteLine(profesor);
                }

            }
        }

        public static void Suma()
        {
            using (var ctx = new cursosEntities())
            {
                var data = ctx.Curso.Average(o=>o.duracion);

                
                    Console.WriteLine(data);
                

            }

        }

        public static void ObjetoDinamico()
        {
            using (var ctx = new cursosEntities())
            {
                var data = ctx.Profesor.Where(o => o.nombre.Contains("Luis"))
                    .Select(o=>new {Denominacion=o.nombre,Antiguedad=o.edad});

                var data2 = from o in ctx.Profesor
                    where o.nombre.Contains("Luis")
                    select new
                    {
                        Denominacion = o.nombre,
                        Antiguedad = o.edad
                    };

                foreach (var profesor in data)
                {
                    Console.WriteLine(profesor);
                }

            }

        }

        public static void BusquedaEnlazada()
        {

            using (var ctx = new cursosEntities())
            {
                
                
                var curpro = ctx.ProfesorCurso.Where(o => o.idProfesor == 1)
                    .Select(o => o.Curso);//.ToList().AsQueryable();

                curpro = curpro.Where(o => o.inicio == DateTime.Now)
                    .OrderBy(o => o.duracion);

                var rf=curpro.Where(o => o.Alumno.Select(oo => oo.dni)
                                        .Contains("1234A")).ToArray();

            }
            

        }
        public static void Subselect()
        {

            using (var ctx = new cursosEntities(false))
            {

                var curpro = ctx.Alumno.Find("12345678A").Curso.Select(o => o.ProfesorCurso.Select(oo=>oo.Profesor));
                    

            }


        }
        public static void SinLazy()
        {

            using (var ctx = new cursosEntities())
            {
                ctx.Configuration.LazyLoadingEnabled = false;

                var alu = ctx.Alumno.Include("Curso").
                    Where(o => o.dni.Contains("A"));



            }


        }
    }
}
