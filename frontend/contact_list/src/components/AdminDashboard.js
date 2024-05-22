import React, { useState, useEffect } from "react";
import axios from "./axiosConfig";
import { Link } from "react-router-dom";

function ContactList() {
  const [contacts, setContacts] = useState([]);

  useEffect(() => {
    fetchContacts();
  }, []);

  // Fetch contacts
  const fetchContacts = async () => {
    try {
      const response = await axios.get("http://localhost:5217/api/Contacts");
      setContacts(response.data);
    } catch (error) {
      console.error("Error fetching contacts:", error);
    }
  };

  // Handle delete
  const handleDelete = async (id) => {
    try {
      await axios.delete(`http://localhost:5217/api/Contacts/${id}`);
      fetchContacts();
    } catch (error) {
      console.error("Error deleting contact:", error);
    }
  };

  return (
    <div>
      <h2>Admin</h2>
      <div className="mb-3">
        <Link to="/create-contact" className="btn btn-primary">
          Create Contact
        </Link>
      </div>
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
            <th>Action</th>
          </tr>
        </thead>
        <tbody>
          {contacts.map((contact) => (
            <tr key={contact.id}>
              <td>{contact.id}</td>
              <td>{contact.firstName}</td>
              <td>{contact.lastName}</td>
              <td>{contact.email}</td>
              <td>{contact.phoneNumber}</td>
              <td>{contact.category.name}</td>
              <td>{contact.subcategory ? contact.subcategory.name : ""}</td>
              <td>{new Date(contact.birthDate).toLocaleDateString()}</td>
              <td>
                <button
                  className="btn btn-danger"
                  onClick={() => handleDelete(contact.id)}
                >
                  Delete
                </button>
                <Link
                  to={`/edit/${contact.id}`}
                  className="btn btn-primary ms-2"
                >
                  Edit
                </Link>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
}

export default ContactList;
