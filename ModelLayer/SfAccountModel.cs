using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLayer
{
    public class SfAccountModel
    {
        private string IdAccount;
        private string Nschool;
        private string Ncontact;
        private string ContactId;
        public List<PopulationModel> Population { get; set; }
        public ubication Location { get; set; }

        public string SchoolName
        {
            get
            {
                return Nschool;
            }
            set
            {
                Nschool = value;
            }
        }

        public string ContactName
        {
            get
            {
                return Ncontact;
            }
            set
            {
                Ncontact = value;
            }
        }

        public string AccountId
        {
            get
            {
                return IdAccount;
            }
            set
            {
                IdAccount = value;
            }
        }

        public string IdContact
        {
            get
            {
                return ContactId;
            }
            set
            {
                ContactId = value;
            }
        }
        

        public class ubication
        {
            public string Latitude { get; set; }
            public string Longitude { get; set; }
        }
    }
}
