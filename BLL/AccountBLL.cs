using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Repository;
using ModelLayer;
using ModelLayer.ViewModels;

namespace BLL

{
    public class AccountBLL
    {
        IGenericRepository<Account> _accountDAL = new GenericRepository<Account>();
        IGenericRepository<AccountConfirmation> _accountConfirmationDAL = new GenericRepository<AccountConfirmation>();
        IGenericRepository<Education> _educationDAL = new GenericRepository<Education>();
        IGenericRepository<Experience> _experienceDAL = new GenericRepository<Experience>();
        IGenericRepository<SocialLink> _socialLinks = new GenericRepository<SocialLink>();

        public string CreateAccount(RegisterVM registerVM, string userRole)
        {
            if (_accountDAL.FindByQueryable(x => x.Email == registerVM.Email).Any())
            {
                return "Email already registered.";
            }
            Account account = new Account();
            account.Name = registerVM.Name;
            account.Email = registerVM.Email;
            account.IsActive = false;           
            account.Id = Guid.NewGuid();
            account.UserRole = userRole;
            account.Salt = CommonBLL.CreateSalt();
            account.Password = CommonBLL.CreatePasswordHash(registerVM.Password, account.Salt);
            //account.ImagePath = "~/Content/img/default.jpg";
            
            account.AccountConfirmation = new AccountConfirmation();
            _accountDAL.Insert(account);
            _accountDAL.Save();
            EmailBLL.SendConfirmationEmail(account.Email, account.Id, account.Name);
            return "";
        }

        public Account Login(LoginVM loginVM)
        {
            Account account=_accountDAL.FindByQueryable(x => x.Email == loginVM.Email).FirstOrDefault();
            string pwd = CommonBLL.CreatePasswordHash(loginVM.Password, account.Salt);
            if (account!=null && account.Password == pwd)
            {
                return account;
            }
            return null;
        }

        public int ConfirmUser(String guid)
        {
            Guid convertedGuid = Guid.Parse(guid);
            AccountConfirmation accountConfirmation = _accountConfirmationDAL.FindByQueryable(x => x.Id == convertedGuid).FirstOrDefault();

            if (accountConfirmation == null)
            {
                return -1;
            }
            Account account = _accountDAL.GetById(accountConfirmation.Id);
            account.IsActive = true;
            _accountDAL.Update(account);
            _accountDAL.Save();

            _accountConfirmationDAL.Delete(accountConfirmation);
            _accountConfirmationDAL.Save();
            
            return 1;
        }

        public DashboardVM DeveloperDetails(Guid id)
        {

            List<Education> educationDetails = _educationDAL.FindByQueryable(x => x.AccountId == id).ToList();
            List<Experience> experienceDetails = _experienceDAL.FindByQueryable(x => x.AccountId == id).ToList();

            return  new DashboardVM { Educations = educationDetails, Experiences = experienceDetails };

        }

        public string GetUserRole(string userName)
        {
            return _accountDAL.FindByQueryable(x => x.Email == userName).Select(x=>x.UserRole).FirstOrDefault();
        }

        public string ForgetPassword(string email)
        {
            Account account=_accountDAL.FindByQueryable(x => x.Email == email).FirstOrDefault();
            if (account == null)
            {
                return "notfound";
            }
            account.ResetCode = Guid.NewGuid().ToString();
            _accountDAL.Update(account);
           
            if (_accountDAL.Save() > 0)
            {
                EmailBLL.ResetPasswordEmail(email, account.ResetCode);
            }
            return "success";
        }

        public bool IsConfirmedForPasswordChange(string guid, string email)
        {
           return _accountDAL.FindByQueryable(x => x.ResetCode == guid && x.Email == email).Any();
        }

        public string ResetPassword(ResetPasswordVM resetPasswordVM)
        {
           Account account = _accountDAL.FindByQueryable(x => x.ResetCode == resetPasswordVM.Guid && x.Email == resetPasswordVM.Email).FirstOrDefault();
            if(account == null)
            {
                return "invalid";
            }
            account.Salt = CommonBLL.CreateSalt();
            account.Password = CommonBLL.CreatePasswordHash(resetPasswordVM.NewPassword, account.Salt);
            account.ResetCode = null;
            _accountDAL.Update(account);
            
            if(_accountDAL.Save() > 0)
            {
                EmailBLL.PasswordChangeEmail(resetPasswordVM.Email);
            }
            return "success";
            
        }

        public void DeleteAccount(Guid id)
        {
            Account account=_accountDAL.GetById(id);
            if (account.SocialLinks.Count > 0)
            {
                _socialLinks.DeleteRange(account.SocialLinks.ToList());
                
            }
        }
    }
}
