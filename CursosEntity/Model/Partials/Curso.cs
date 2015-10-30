using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace CursosEntity.Model
{
   public partial class Curso
    {
       public override string ToString()
       {
           return $"{nombre} {duracion}";
       }
    }
}
