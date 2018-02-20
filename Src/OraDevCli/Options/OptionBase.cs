using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OraDevCli.Options
{
    public abstract class OptionBase : IOption
    {

        [Option('u', HelpText = "The database will be linked to this user. Default user is the current user.", Required = false)]
        public string User { get; set; }
        [Option('n', HelpText = "The database name. Default value is default", Required = false)]
        public string Name { get; set; }
        public string ContainerKey { get; set; } = "ora11";

        private string GetUSer()
        {
            if (string.IsNullOrEmpty(User))
            {
                string fullName = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                return fullName.Split('\\').Last().ToLower();
            }
            return User.ToLower();
        }

        private string GetName()
        {

            if (string.IsNullOrEmpty(Name))
            {
                return "Default".ToLower();
            }
            return Name.ToLower();
        }

        public virtual void Run()
        {
            Name = GetName();
            User = GetUSer();
            Console.WriteDebugLine($"User is {User}");
            Console.WriteDebugLine($"Name is {Name}");
        }
    }
}
