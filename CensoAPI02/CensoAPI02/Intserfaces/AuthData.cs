using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CensoAPI02.Intserfaces
{
    public class AdministratorAuthData
    {
        // Atibutos del administrador
        private int adminId { get; set; }

        private long adminEmployeeNumber { get; set; }

        public long adminSupervisorNumber { get; set; }

        private int adminRole { get; set; }

        private int adminLocation { get; set; }

        private string adminName { get; set; }

        private string adminEmail { get; set; }

        // Setter methods
        public void setAdminId(int adminId)
        {
            this.adminId = adminId;
        }

        public void setAdminEmployeeNumber(long adminEmployeeNumber)
        {
            this.adminEmployeeNumber = adminEmployeeNumber;
        }

        public void setAdminSupervisorNumber(long adminSupervisorNumber)
        {
            this.adminSupervisorNumber = adminSupervisorNumber;
        }

        public void setAdminRole(int adminRole)
        {
            this.adminRole = adminRole;
        }

        public void setAdminLocation(int adminLocation)
        {
            this.adminLocation = adminLocation;
        }

        public void setAdminName(string adminName)
        {
            this.adminName = adminName;
        }

        public void setAdminEmail(string adminEmail)
        {
            this.adminEmail = adminEmail;
        }

        // Getter methods
        public int getAdminId()
        {
            return this.adminId;
        }

        public long getAdminEmployeeNumber()
        {
            return this.adminEmployeeNumber;
        }

        public long getSupervisorNumber()
        {
            return this.adminSupervisorNumber;
        }

        public int getAdminRole()
        {
            return this.adminRole;
        }

        public int getAdminLocation()
        {
            return this.adminLocation;
        }

        public string getAdminName()
        {
            return this.adminName;
        }

        public string getAdminEmail()
        {
            return this.adminEmail;
        }
        
    }


    public class UserAuthData
    {
        // Atributos del ususario
        private int EmployeeNumber { get; set; }

        public string Email { get; set; }

        //private string Location { get; set; }

        private int Location { get; set; }

        private string Name { get; set; }

        private int supervisorNumber { get; set; }

        // Setter methods
        public void setUserEmployeeNumer(int EmployeeNumber)
        {
            this.EmployeeNumber = EmployeeNumber;
        }

        public void setUserEmail(string Email)
        {
            this.Email = Email;
        }

        public void setUserLocation(int Location)
        {
            this.Location = Location;
        }

        public void setUsername(string Name)
        {
            this.Name = Name;
        }

        public void setSupervisorNumber(int supervisorNumber)
        {
            this.supervisorNumber = supervisorNumber;
        }

        // Getter methods
        public int getUserEmployeeNumber()
        {
            return this.EmployeeNumber;
        }

        public string getUserEmail()
        {
            return this.Email;
        }

        public int getUserLocation()
        {
            return this.Location;
        }

        public string getUsername()
        {
            return this.Name;
        }

        public int getSupervisorNumber()
        {
            return this.supervisorNumber;
        }
    }
}
