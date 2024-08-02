using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelLayer;
using ModelLayer.ViewModels;
using DAL.Repository;
using System.Web;
using System.IO;
using System.Collections;

namespace BLL
{
    public class ProfileBLL
    {
        IGenericRepository<Profile> _profileDAL = new GenericRepository<Profile>();
        IGenericRepository<Account> _accountDAL = new GenericRepository<Account>();
        IGenericRepository<SocialLink> _socialLinksDAL = new GenericRepository<SocialLink>();
        IGenericRepository<Profession> _professionDAL = new GenericRepository<Profession>();
        IGenericRepository<Experience> _experienceDAL = new GenericRepository<Experience>();
        IGenericRepository<Education> _educationDAL = new GenericRepository<Education>();


        public Profile GetProfile(Guid id)
        {
            return _profileDAL.GetById(id);

        }


        public ProfileVM GetProfileForEdit(Guid id)
        {
            Profile profile = _profileDAL.GetById(id);
            ProfileVM profileVM = new ProfileVM();
            if (profile != null)
            {
                profileVM.Imagepath = profile.Account.ImagePath;
                profileVM.Address = profile.Address;
                profileVM.Bio = profile.Bio;
                profileVM.GitHubUsername = profile.GitHubUsername;
                profileVM.ProfessionalStatusId = profile.ProfessionId;
                profileVM.Skills = profile.Skills;
                profileVM.Website = profile.Website;
            }

            return profileVM;
        }


        public string EditProfile(ProfileVM profileVM, Guid id)
        {
            Account account = _accountDAL.GetById(id);
            if (account.Profile != null)
            {
                account.Profile.Address = profileVM.Address;
                account.Profile.ProfessionId = profileVM.ProfessionalStatusId;
                account.Profile.Website = profileVM.Website;
                account.Profile.Skills = profileVM.Skills;
                account.Profile.GitHubUsername = profileVM.GitHubUsername;
                account.Profile.Bio = profileVM.Bio;
            }

            else
            {
                Profile profile = new Profile
                {
                    AccountId = id,
                    ProfessionId = profileVM.ProfessionalStatusId,
                    Website = profileVM.Website,
                    Address = profileVM.Address,
                    Skills = profileVM.Skills,
                    GitHubUsername = profileVM.GitHubUsername,
                    Bio = profileVM.Bio

                };
                account.Profile = profile;

            }
            if (profileVM.Image != null)
            {

                string imagePath = CommonBLL.UploadProfileImage(profileVM.Image, "~/Content/img/");
                if (imagePath.Contains("/"))
                {
                    if (account.ImagePath != null)
                    {
                        File.Delete(HttpContext.Current.Server.MapPath(account.ImagePath));
                    }
                    account.ImagePath = imagePath;
                }
                else
                {
                    return imagePath;
                }
            }

            _accountDAL.Update(account);

            _accountDAL.Save();
            return "success";

        }


        public List<SocialLink> GetSocialLinks(Guid accountId)
        {
            return _socialLinksDAL.FindByQueryable(x => x.AccountId == accountId).ToList();
        }

        public void UpdateSocialLinks(SocialLinksVM socialLinksVM, Guid accountId)
        {
            List<SocialLink> socialLinks = _socialLinksDAL.FindByQueryable(x => x.AccountId == accountId).ToList();
            if (socialLinks.Count > 0)
            {

                foreach (var item in socialLinks)
                {
                    if (item.Link.Contains("facebook"))
                    {
                        item.Link = "facebook.com/" + socialLinksVM.Facebook;
                    }
                    else if (item.Link.Contains("twitter"))
                    {
                        item.Link = "twitter.com/" + socialLinksVM.Twitter;
                    }
                    else if (item.Link.Contains("youtube"))
                    {
                        item.Link = "youtube.com/" + socialLinksVM.Youtube;
                    }
                    else if (item.Link.Contains("instagram"))
                    {
                        item.Link = "instagram.com/" + socialLinksVM.Instagram;
                    }
                    else if (item.Link.Contains("linkedin"))
                    {
                        item.Link = "linkedin.com/" + socialLinksVM.Linkedin;
                    }

                    _socialLinksDAL.Update(item);
                }

                List<SocialLink> newSocialLinks = new List<SocialLink>();
                if (!socialLinks.Where(x => x.Link.Contains("facebook")).Any() && !string.IsNullOrEmpty(socialLinksVM.Facebook))
                {
                    newSocialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "facebook.com/" + socialLinksVM.Facebook });

                }
                if (!socialLinks.Where(x => x.Link.Contains("twitter")).Any() && !string.IsNullOrEmpty(socialLinksVM.Twitter))
                {
                    newSocialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "twitter.com/" + socialLinksVM.Twitter });

                }
                if (!socialLinks.Where(x => x.Link.Contains("linkedin")).Any() && !string.IsNullOrEmpty(socialLinksVM.Linkedin))
                {
                    newSocialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "linkedin.com/" + socialLinksVM.Linkedin });

                }
                if (!socialLinks.Where(x => x.Link.Contains("youtube")).Any() && !string.IsNullOrEmpty(socialLinksVM.Youtube))
                {
                    newSocialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "youtube.com/" + socialLinksVM.Youtube });

                }
                if (!socialLinks.Where(x => x.Link.Contains("instagram")).Any() && !string.IsNullOrEmpty(socialLinksVM.Instagram))
                {
                    newSocialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "instagram.com/" + socialLinksVM.Instagram });

                }
                if (newSocialLinks.Count > 0)
                {
                    _socialLinksDAL.AddRange(newSocialLinks);

                }
                _socialLinksDAL.Save();

            }
            else
            {
                if (!string.IsNullOrEmpty(socialLinksVM.Facebook))
                {
                    socialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "facebook.com/" + socialLinksVM.Facebook });

                }
                if (!string.IsNullOrEmpty(socialLinksVM.Twitter))
                {
                    socialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "twitter.com/" + socialLinksVM.Twitter });

                }
                if (!string.IsNullOrEmpty(socialLinksVM.Linkedin))
                {
                    socialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "linkedin.com/" + socialLinksVM.Linkedin });

                }
                if (!string.IsNullOrEmpty(socialLinksVM.Youtube))
                {
                    socialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "youtube.com/" + socialLinksVM.Youtube });

                }
                if (!string.IsNullOrEmpty(socialLinksVM.Instagram))
                {
                    socialLinks.Add(new SocialLink { Id = Guid.NewGuid(), AccountId = accountId, Link = "instagram.com/" + socialLinksVM.Instagram });

                }
                _socialLinksDAL.AddRange(socialLinks);
                _socialLinksDAL.Save();
            }

        }

        public List<Profession> GetAllProfessions()
        {
            return _professionDAL.GetAllQueryable().ToList();

        }

        public int AddExperience(AddexperienceVM addexperienceVM, Guid id)
        {
            _experienceDAL.Insert(new Experience
            {
                Id = Guid.NewGuid(),
                AccountId = id,
                JobTitle = addexperienceVM.JobTitle,
                Company = addexperienceVM.Company,
                Location = addexperienceVM.Location,
                FromDate = addexperienceVM.FromDate,
                ToDate = addexperienceVM.ToDate,
                IsCurrentJob = addexperienceVM.ToDate == null ? true : false,
                JobDescription = addexperienceVM.JobDescription,
                Position = addexperienceVM.Position
            });
            return _experienceDAL.Save();


        }

        public int AddEducation(AddEducationVM addEducationVM, Guid id)
        {
            _educationDAL.Insert(new Education
            {
                Id = Guid.NewGuid(),
                AccountId = id,
                School = addEducationVM.School,
                Degree = addEducationVM.Degree,
                FieldOfStudy = addEducationVM.FieldOfStudy,
                FromDate = addEducationVM.FromDate,
                ToDate = addEducationVM.ToDate,
                IsCurrent = addEducationVM.ToDate == null ? true : false,
                Description = addEducationVM.Description
            });
            return _educationDAL.Save();


        }

        public List<DevelopersVM> GetAllProfiles()
        {
            return _profileDAL.GetAllQueryable().Select(x => new DevelopersVM
            {
                AccountId = x.AccountId,
                Address = x.Address,
                Company = x.Account.Experiences.FirstOrDefault(y => y.IsCurrentJob == true).Company,
                Imagepath = x.Account.ImagePath,
                Name = x.Account.Name,
                ProfessionalStatus = x.Profession.ProfessionalStatus,
                Skills = x.Skills
            }).ToList();
        }

        public IQueryable<Profile> FilterDevelopers(string skill)
        {

            string[] skills = null;
            string skill0 = null;
            int i = 0;
            if (skill.Contains(","))
            {
                skills = skill.Split(',');
                i = skills.Length;
            }
            else
            {
                skill0 = skill;
            }
            
            string skill1 = null;
            string skill2 = null;
            string skill3 = null;
            string skill4 = null;
            string skill5 = null;
            string skill6 = null;
            string skill7 = null;
            switch (i)
            {
                case 0:
                    return _profileDAL.FindByQueryable(x => x.Skills.Contains(skill0.Trim()));

                case 2:
                    skill1 = skills[0].Trim();
                    skill2 = skills[1].Trim();

                    return _profileDAL.FindByQueryable(x => x.Skills.Contains(skill1) && x.Skills.Contains(skill2));
                    
                //case 3:
                //    skill1 = skills[0].Trim();
                //    skill2 = skills[1].Trim();
                //    skill3 = skills[2].Trim();
                //    return _profileDAL.FindByQueryable(x => x.Skills.Contains(skill1.Trim()) && x.Skills.Contains(skills[1].Trim()) && x.Skills.Contains(skills[2].Trim()));

                //case 4:
                //    skill1 = skills[0].Trim();
                //    skill2 = skills[1].Trim();
                //    skill3= skills[2].Trim();
                //    skill4= skills[3].Trim();
                //    return _profileDAL.FindByQueryable(x => x.Skills.Contains(skills[0].Trim()) && x.Skills.Contains(skills[1].Trim()) && x.Skills.Contains(skills[2].Trim()) && x.Skills.Contains(skills[3].Trim()));

                //case 5:
                //    skill1 = skills[0].Trim();
                //    skill2 = skills[1].Trim();
                //    skill3 = skills[2].Trim();
                //    skill4 = skills[3].Trim();
                //    skill5 = skills[4].Trim();
                   
                //    return _profileDAL.FindByQueryable(x => x.Skills.Contains(skills[0].Trim()) && x.Skills.Contains(skills[1].Trim()) && x.Skills.Contains(skills[2].Trim()) && x.Skills.Contains(skills[3].Trim()) && x.Skills.Contains(skills[4].Trim()));

                //case 6:
                //    skill1 = skills[0].Trim();
                //    skill2 = skills[1].Trim();
                //    skill3 = skills[2].Trim();
                //    skill4 = skills[3].Trim();
                //    skill5 = skills[4].Trim();
                //    skill6 = skills[5].Trim();
                //    return _profileDAL.FindByQueryable(x => x.Skills.Contains(skills[0].Trim()) && x.Skills.Contains(skills[1].Trim()) && x.Skills.Contains(skills[2].Trim()) && x.Skills.Contains(skills[3].Trim()) && x.Skills.Contains(skills[4].Trim()) && x.Skills.Contains(skills[5].Trim()));

                //case 7:
                //    skill1 = skills[0].Trim();
                //    skill2 = skills[1].Trim();
                //    skill3 = skills[2].Trim();
                //    skill4 = skills[3].Trim();
                //    skill5 = skills[4].Trim();
                //    skill6 = skills[5].Trim();
                //    skill7 = skills[6].Trim();
                //    return _profileDAL.FindByQueryable(x => x.Skills.Contains(skill0.Trim()) && x.Skills.Contains(skills[1].Trim()) && x.Skills.Contains(skills[2].Trim()) && x.Skills.Contains(skills[3].Trim()) && x.Skills.Contains(skills[4].Trim()) && x.Skills.Contains(skills[5].Trim()) && x.Skills.Contains(skills[6].Trim()));

                default:
                    return null;
            }


        }

        public int DeleteEducation(string id)
        {
           
             _educationDAL.Delete(_educationDAL.GetById(Guid.Parse(id)));
            return _educationDAL.Save();
        }

        public int DeleteExperience(string id)
        {
            _experienceDAL.Delete(_experienceDAL.GetById(Guid.Parse(id)));
            return _experienceDAL.Save();
        }
    }
}
