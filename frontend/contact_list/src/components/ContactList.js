import React, { useState, useEffect } from 'react';
import axios from 'axios';

function ContactList() {
  const [contacts, setContacts] = useState([]);

  useEffect(() => {
    fetchContacts();
  }, []);

  // Fetch contacts
  const fetchContacts = async () => {
    try {
      const response = await axios.get('http://localhost:5217/api/Contacts');
      setContacts(response.data);
    } catch (error) {
      console.error('Error fetching contacts:', error);
    }
  };

  return (
    <div>
      <h2>Contact List</h2>
      <table className="table table-striped">
        <thead>
          <tr>
            <th>ID</th>
            <th>First Name</th>
            <th>Last Name</th>
            <th>Email</th>
            <th>Phone Number</th>
            <th>Category</th>
            <th>Subcategory</th>
            <th>Birth Date</th>
          </tr>
        </thead>
        <tbody>
          {contacts.map(contact => (
            <tr key={contact.id}>
              <td>{contact.id}</td>
              <td>{contact.firstName}</td>
              <td>{contact.lastName}</td>
              <td>{contact.email}</td>
              <td>{contact.phoneNumber}</td>
              <td>{contact.category.name}</td>
              <td>{contact.subcategory ? contact.subcategory.name : ''}</td>
              <td>{new Date(contact.birthDate).toLocaleDateString()}</td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ContactList;
