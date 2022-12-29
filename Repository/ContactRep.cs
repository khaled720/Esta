using System.IO;
using ESTA.Models;
using ESTA.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ESTA.Repository
{
    public class ContactRep : IContactRep
    {
        private readonly AppDbContext appContext;

        public ContactRep(AppDbContext appContext)
        {
            this.appContext = appContext;

        }
        public async Task<bool> AddContact(Contact contact)
        {
            try
            {
                await appContext.Contacts.AddAsync(contact);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteContact(int id)
        {
            try
            {
                appContext.Contacts.Remove(appContext.Contacts.First(t => t.Id == id));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditContact(Contact contact)
        {
            try
            {
                var dbContact = await appContext.Contacts.AsTracking().FirstAsync(y => y.Id == contact.Id);

                dbContact.PhoneLines = contact.PhoneLines;
                dbContact.Emails = contact.Emails;
                dbContact.AddressEn=contact.AddressEn;
                dbContact.AddressAr=contact.AddressAr;
                dbContact.TitleAr=contact.TitleAr;
                dbContact.TitleEn=contact.TitleEn;

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

        public async Task<IEnumerable<Contact>> GetAllContacts()
        {
            return await appContext.Contacts.ToListAsync();
        }

        public async Task<Contact> GetContact(int id)
        {
            try
            {
                return await appContext.Contacts.AsNoTracking().FirstAsync(y => y.Id == id);

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
