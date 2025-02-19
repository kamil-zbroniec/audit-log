const API_URL = import.meta.env.VITE_API_URL;
const API_KEY = import.meta.env.VITE_API_KEY;

export const fetchAuditLogs = async (organisationId = "", startDate = null, endDate = null, pageNumber = 1, pageSize = 10) => {
  const url = new URL(`${API_URL}/audit-log`);
  
  if (organisationId) url.searchParams.append("organizationId", organisationId);
  if (startDate) url.searchParams.append("startDate", startDate);
  if (endDate) url.searchParams.append("endDate", endDate);
  url.searchParams.append("pageNumber", pageNumber);
  url.searchParams.append("pageSize", pageSize);

  const response = await fetch(url, {
    headers: {
      "X-Api-Key": API_KEY,
    },
  });

  if (!response.ok) {
    throw new Error(`Failed to fetch audit logs: ${response.statusText}`);
  }

  return response.json();
};