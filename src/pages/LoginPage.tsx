import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const LoginPage = () => {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const navigate = useNavigate();

  const handleLogin = () => {
    // Prosty mechanizm logowania (do zastąpienia systemem backendowym)
    if (username === 'admin' && password === 'admin') {
      localStorage.setItem('adminToken', 'true'); // Symulacja zalogowania
      navigate('/admin'); // Przekierowanie do panelu admina
    } else {
      alert('Niepoprawne dane logowania!');
    }
  };

  return (
    <div className="flex flex-col items-center justify-center min-h-screen bg-slate-600">
      <h1 className="text-2xl font-bold mb-4">Logowanie Administratora</h1>
      <input
        type="text"
        placeholder="Nazwa użytkownika"
        className="border p-2 mb-2"
        value={username}
        onChange={(e) => setUsername(e.target.value)}
      />
      <input
        type="password"
        placeholder="Hasło"
        className="border p-2 mb-2"
        value={password}
        onChange={(e) => setPassword(e.target.value)}
      />
      <button
        onClick={handleLogin}
        className="bg-blue-500 text-white px-4 py-2 rounded"
      >
        Zaloguj się
      </button>
    </div>
  );
};

export default LoginPage;
