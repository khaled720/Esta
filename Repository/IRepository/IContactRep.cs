using ESTA.Models;

namespace ESTA.Repository.IRepository
{
    public interface IContactRep
    {

        public Task<IEnumerable<Contact>> GetAllContacts();
        public Task<Contact> GetContact(int id);

        public Task<bool> AddContact(Contact contact);

        public Task<bool> EditContact(Contact contact);

        public bool DeleteContact(int id);
    }
}
