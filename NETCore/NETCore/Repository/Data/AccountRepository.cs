using NETCore.Context;
using NETCore.Models;
using NETCore.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NETCore.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        MyContext myContext;
        public AccountRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        private static string GenerateSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        private static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, GenerateSalt());
        }
        private static bool ValidatePassword(string password, string correct)
        {
            return BCrypt.Net.BCrypt.Verify(password, correct);
        }


        public int ForgotPassword(ForgotPasswordVM forgotPassword)
        {
            Account account = new Account();
            var checkEmail = myContext.Persons.Where(p => p.Email == forgotPassword.Email).FirstOrDefault();
            var checkNikAccount = myContext.Accounts.Where(a => a.NIK == checkEmail.NIK).FirstOrDefault();
            var result = 0;
            //generate guid
            string guid = Guid.NewGuid().ToString();
            if (checkEmail == null)
            {
                return result;
            }
            //string mail = $"Email = {checkEmail.Email}  Token={checkEmail.NIK}";
            //account.Password = forgotPassword.NewPassword;
            string mail = $"Password Baru : {guid}";
            string hashPass = HashPassword(guid);
            Email(mail, checkEmail.Email);
            checkNikAccount.Password = hashPass;
            Update(checkNikAccount);
            result = myContext.SaveChanges();
            return result;
        }

        public string[] Roles(string email)
        {
            var all = (from p in myContext.Persons
                       join a in myContext.Accounts on p.NIK equals a.NIK
                       join b in myContext.AccountRole on a.NIK equals b.NIK
                       join c in myContext.Role on b.RoleId equals c.Id
                       where p.Email == email
                       select new Role
                       {
                           Name = c.Name                           
                       }).ToList();
            string[] roles = new string[all.Count];
            for(int i = 0; i < all.Count; i++)
            {
                roles[i] = all[i].Name;
            }
            return roles;
        }

        public int Login(LoginVM login)
        {
            var res = 0;
            var check = myContext.Persons.FirstOrDefault(p => p.Email == login.Email);
            //var checkPas = myContext.Accounts.FirstOrDefault(p => p.Password == login.Password); 
            if (check != null)
            {
                if (ValidatePassword(login.Password, check.Account.Password))
                {
                    res = 2;
                }
                else
                {
                    res = 1;
                }
            }
            return res;
        }

        public int UpdatePassword(ChangePassword changepassword)
        {
            //Account account = new Account();
            var cekemail = myContext.Persons.Where(p => p.Email == changepassword.email).FirstOrDefault();
            //var passvalid = ValidatePassword(changepassword.Password, cekemail.Account.Password);
            //var cekPass = myContext.Accounts.Where(a => a.Password ==  ).FirstOrDefault();
            var result = 0;
            if (cekemail == null)
            {
                //if (ValidatePassword(changepassword.Password, checkNikAccount.Password))
                //{
                return 100;
                //}
                //return 200;
            }
            else
            {
                var checkNikAccount = myContext.Accounts.Where(a => a.NIK == cekemail.NIK).FirstOrDefault();
                var account = myContext.Accounts.Where(x => cekemail.NIK.Equals(x.NIK)).FirstOrDefault();
                if (!ValidatePassword(changepassword.Password, checkNikAccount.Password))
                {
                    return 200;
                }
                else
                {
                    if (changepassword.NewPassword == changepassword.ConfirmPassword)
                    {
                        //string newpass = HashPassword(changepassword.NewPassword);
                        //account.Password = changepassword.NewPassword;
                        //string passbaru = HashPassword(account.Password);
                        checkNikAccount.Password = HashPassword(changepassword.NewPassword);
                        account.Password = checkNikAccount.Password;
                        Update(account);
                        result = myContext.SaveChanges();
                        return result;
                    }
                    else
                    {
                        return 300;
                    }
                }
            }
        }

        public static void Email(string htmlString, string toMailAddress)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("jiebobby3@gmail.com");
                message.To.Add(new MailAddress(toMailAddress));
                DateTime hariini = DateTime.Now;
                message.Subject = $"Reset Password  {(hariini.ToString("MM/dd/yyyy HH:mm"))}";
                message.IsBodyHtml = true;
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("jiebobby3@gmail.com", "slashtheking97");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception)
            {
            }
        }
    }
}
