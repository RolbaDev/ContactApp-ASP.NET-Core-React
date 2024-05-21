import React, { useState, useEffect } from "react";
import { Link, useNavigate } from "react-router-dom";

function Navbar() {
  // State to track if user is authenticated
  const [isAuthenticated, setIsAuthenticated] = useState(
    localStorage.getItem("token") ? true : false
  );

  // Hook for navigation
  const navigate = useNavigate();

  // Function to handle logout
  const handleLogout = () => {
    localStorage.removeItem("token");
    setIsAuthenticated(false);
    navigate("/");
  };

  // Effect hook to handle changes in local storage
  useEffect(() => {
    // Function to handle storage change
    const handleStorageChange = () => {
      const token = localStorage.getItem("token");
      setIsAuthenticated(token ? true : false);
    };

    // Event listener for storage change
    window.addEventListener("storage", handleStorageChange);

    // Cleanup function to remove event listener
    return () => {
      window.removeEventListener("storage", handleStorageChange);
    };
  }, []);

  // Render navbar component
  return (
    <nav className="navbar navbar-expand-lg navbar-light bg-light">
      <div className="container">
        <Link className="navbar-brand" to="/">
          Contact List
        </Link>
        <div className="collapse navbar-collapse" id="navbarNav">
          <ul className="navbar-nav mx-auto">
            <li className="nav-item">
              <Link className="nav-link" to="/">
                Home
              </Link>
            </li>
            {!isAuthenticated && (
              <li className="nav-item">
                <Link className="nav-link" to="/login">
                  Login
                </Link>
              </li>
            )}
            {isAuthenticated && (
              <>
                <li className="nav-item">
                  <Link className="nav-link" to="/admin-dashboard">
                    Admin Dashboard
                  </Link>
                </li>
                <li className="nav-item">
                  <button
                    className="nav-link btn btn-link"
                    onClick={handleLogout}
                  >
                    Logout
                  </button>
                </li>
              </>
            )}
          </ul>
        </div>
      </div>
    </nav>
  );
}

export default Navbar;
