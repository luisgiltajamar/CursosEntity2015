using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursosEntity.Model
{
   public partial class cursosEntities
    {
       public cursosEntities(bool lazy) : base("name=cursosEntities")
        {
           Configuration.LazyLoadingEnabled = lazy;
       }
    }
}
