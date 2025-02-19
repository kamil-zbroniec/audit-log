export const fetchOrganizations = async () => {
  const API_URL = import.meta.env.VITE_API_URL;
  const API_KEY = import.meta.env.VITE_API_KEY;

  const response = await fetch(`${API_URL}/organization`, {
    headers: {
      "X-Api-Key": API_KEY,
    },
  });
  return response.json();
};

