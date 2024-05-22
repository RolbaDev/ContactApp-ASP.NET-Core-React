import React, { useState, useEffect } from "react";
import axios from "./axiosConfig"; // Importuj skonfigurowany axios
import { useNavigate } from "react-router-dom";

function CreateContact() {
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    password: "",
    categoryId: 0,
    subcategoryId: "",
    subcategoryName: "",
    phoneNumber: "",
    birthDate: "",
  });
  const [categories, setCategories] = useState([]);
  const [subcategories, setSubcategories] = useState([]);
  const [subcategories2, setSubcategories2] = useState([]);
  const navigate = useNavigate();
  const [showSubcategory, setShowSubcategory] = useState(true);
  const [showSubcategoryInput, setShowSubcategoryInput] = useState(false);
  const [showSubcategoryNameInput, setShowSubcategoryNameInput] = useState(false);

  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const response = await axios.get("http://localhost:5217/api/Categories");
      setCategories(response.data);
    } catch (error) {
      console.error("Error fetching categories:", error);
    }
  };

  const fetchSubcategories = async () => {
    try {
      const response = await axios.get("http://localhost:5217/api/Subcategories");
      const filteredSubcategories = response.data.filter(
        (subcategory) => subcategory.name === "szef" || subcategory.name === "klient"
      );
      setSubcategories(filteredSubcategories);
    } catch (error) {
      console.error("Error fetching subcategories:", error);
    }
  };

  const fetchSubcategories2 = async () => {
    try {
      const response = await axios.get("http://localhost:5217/api/Subcategories");
      setSubcategories2(response.data);
      console.log("Existing subcategory:", response.data);
    } catch (error) {
      console.error("Error fetching subcategories:", error);
    }
  };

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData((prevFormData) => ({
      ...prevFormData,
      [name]: name === "categoryId" || name === "subcategoryId" ? parseInt(value, 10) : value,
    }));

    if (name === "categoryId") {
      const selectedCategory = categories.find(
        (category) => category.id === parseInt(value, 10)
      );
      if (selectedCategory?.name === "prywatny") {
        setShowSubcategory(false);
        setShowSubcategoryInput(false);
        setShowSubcategoryNameInput(false);
        setFormData((prevFormData) => ({
          ...prevFormData,
          subcategoryId: "",
          subcategoryName: "",
        }));
        fetchSubcategories2();
      } else if (selectedCategory?.name === "służbowy") {
        setShowSubcategory(true);
        setShowSubcategoryInput(false);
        setShowSubcategoryNameInput(false);
        fetchSubcategories();
        fetchSubcategories2();
      } else if (selectedCategory?.name === "Inny") {
        setShowSubcategory(true);
        setShowSubcategoryInput(false);
        setShowSubcategoryNameInput(true);
        fetchSubcategories2();
        setFormData((prevFormData) => ({
          ...prevFormData,
          subcategoryId: "",
          subcategoryName: "",
        }));
      } else {
        setShowSubcategory(true);
        setShowSubcategoryInput(false);
        setShowSubcategoryNameInput(false);
        setFormData((prevFormData) => ({
          ...prevFormData,
          subcategoryId: "",
          subcategoryName: "",
        }));
      }
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    try {
      const isoDate = new Date(formData.birthDate).toISOString();
      let subcategoryId = formData.subcategoryId;

      fetchSubcategories2();

      if (showSubcategoryNameInput && formData.subcategoryName) {
        const existingSubcategory = subcategories2.find(
          (subcategory) => subcategory.name === formData.subcategoryName
        );

        if (existingSubcategory) {
          subcategoryId = existingSubcategory.id;
        } else {
          const response = await axios.post("http://localhost:5217/api/Subcategories", {
            name: formData.subcategoryName,
          });
          subcategoryId = response.data.id;
        }
      }

      const updatedFormData = {
        ...formData,
        birthDate: isoDate,
        subcategoryId: showSubcategory ? subcategoryId : null,
        subcategoryName: undefined,
      };

      await axios.post("http://localhost:5217/api/Contacts", updatedFormData);
      navigate("/admin-dashboard");
    } catch (error) {
      console.error("Error creating contact:", error);
    }
  };

  return (
    <div className="container">
      <h2>Create New Contact</h2>
      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label htmlFor="firstName" className="form-label">
            First Name
          </label>
          <input
            type="text"
            className="form-control"
            id="firstName"
            name="firstName"
            value={formData.firstName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="lastName" className="form-label">
            Last Name
          </label>
          <input
            type="text"
            className="form-control"
            id="lastName"
            name="lastName"
            value={formData.lastName}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="email" className="form-label">
            Email
          </label>
          <input
            type="email"
            className="form-control"
            id="email"
            name="email"
            value={formData.email}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="password" className="form-label">
            Password
          </label>
          <input
            type="password"
            className="form-control"
            id="password"
            name="password"
            value={formData.password}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="phoneNumber" className="form-label">
            Phone Number
          </label>
          <input
            type="text"
            className="form-control"
            id="phoneNumber"
            name="phoneNumber"
            value={formData.phoneNumber}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="birthDate" className="form-label">
            Birth Date
          </label>
          <input
            type="date"
            className="form-control"
            id="birthDate"
            name="birthDate"
            value={formData.birthDate}
            onChange={handleChange}
            required
          />
        </div>
        <div className="mb-3">
          <label htmlFor="categoryId" className="form-label">
            Category
          </label>
          <select
            className="form-control"
            id="categoryId"
            name="categoryId"
            value={formData.categoryId}
            onChange={handleChange}
            required
          >
            <option value="">Select Category</option>
            {categories.map((category) => (
              <option key={category.id} value={category.id}>
                {category.name}
              </option>
            ))}
          </select>
        </div>
        {showSubcategory && (
          <>
            {showSubcategoryInput ? (
              <div className="mb-3">
                <label htmlFor="subcategoryId" className="form-label">
                  Subcategory ID
                </label>
                <input
                  type="number"
                  className="form-control"
                  id="subcategoryId"
                  name="subcategoryId"
                  value={formData.subcategoryId}
                  onChange={handleChange}
                />
              </div>
            ) : showSubcategoryNameInput ? (
              <div className="mb-3">
                <label htmlFor="subcategoryName" className="form-label">
                  Subcategory Name
                </label>
                <input
                  type="text"
                  className="form-control"
                  id="subcategoryName"
                  name="subcategoryName"
                  value={formData.subcategoryName}
                  onChange={handleChange}
                />
              </div>
            ) : (
              <div className="mb-3">
                <label htmlFor="subcategoryId" className="form-label">
                  Subcategory
                </label>
                <select
                  className="form-control"
                  id="subcategoryId"
                  name="subcategoryId"
                  value={formData.subcategoryId}
                  onChange={handleChange}
                >
                  <option value="">Select Subcategory</option>
                  {subcategories.map((subcategory) => (
                    <option key={subcategory.id} value={subcategory.id}>
                      {subcategory.name}
                    </option>
                  ))}
                </select>
              </div>
            )}
          </>
        )}
        <button type="submit" className="btn btn-primary">
          Create
        </button>
      </form>
    </div>
  );
}

export default CreateContact;
