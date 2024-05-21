import {
  BrowserRouter as Router,
  Routes,
  Route,
  Navigate,
} from "react-router-dom";
import React, { useState, useEffect } from "react";
import "./App.css";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";
import Login from "./components/Login";
import ContactList from "./components/ContactList";
import AdminDashboard from "./components/AdminDashboard";
import CreateContact from "./components/CreateContact";
import EditContact from "./components/EditContact";

function App() {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  useEffect(() => {
    // Function to check for authentication token in localStorage
    const token = localStorage.getItem("token");

    setIsAuthenticated(!!token);

    // Listener for changes in localStorage (e.g., token updates)
    const storageListener = () => {
      const newToken = localStorage.getItem("token");
      setIsAuthenticated(!!newToken);
    };

    window.addEventListener("storage", storageListener);

    // Cleanup: remove event listener when component unmounts
    return () => {
      window.removeEventListener("storage", storageListener);
    };
  }, []);

  // Rendering routes based on authentication status
  return (
    <Router>
      <div className="d-flex flex-column min-vh-100">
        <Navbar />
        <div className="container flex-grow-1">
          <Routes>
            <Route
              path="/login"
              element={isAuthenticated ? <Navigate to="/" /> : <Login />}
            />
            <Route path="/" element={<ContactList />} />
            <Route
              path="/admin-dashboard"
              element={
                isAuthenticated ? <AdminDashboard /> : <Navigate to="/login" />
              }
            />
            <Route
              path="/create-contact"
              element={
                isAuthenticated ? <CreateContact /> : <Navigate to="/login" />
              }
            />
            <Route
              path="/edit/:id"
              element={
                isAuthenticated ? <EditContact /> : <Navigate to="/login" />
              }
            />
          </Routes>
        </div>
        <Footer />
      </div>
    </Router>
  );
}

export default App;
