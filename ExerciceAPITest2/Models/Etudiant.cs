using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExerciceAPITest2.Models
{
    public class Etudiant
    {
        public int Id { get; set; }
        public string firstName { get; set; }

        public string lastName { get; set; }   

        public int filiere_id { get; set; }

    }
}