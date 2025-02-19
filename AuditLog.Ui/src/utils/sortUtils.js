export const getSortIndicator = (currentKey, sortConfig) => {
  if (sortConfig.key !== currentKey) return '↕';
  return sortConfig.direction === 'asc' ? '↑' : '↓';
};

export const sortData = (data, key, direction, organizations) => {
  return [...data].sort((a, b) => {
    if (key === 'organization') {
      const aVal = organizations[a.organizationId] || '';
      const bVal = organizations[b.organizationId] || '';
      return ((aVal > bVal) - (aVal < bVal)) * (direction === 'asc' ? 1 : -1);
    }
    
    if (key === 'entries') {
      return ((a[key]?.length || 0) - (b[key]?.length || 0)) * (direction === 'asc' ? 1 : -1);
    }

    if (key === 'startDate') {
      return (new Date(a[key]) - new Date(b[key])) * (direction === 'asc' ? 1 : -1);
    }

    const aVal = a[key] || '';
    const bVal = b[key] || '';
    return ((aVal > bVal) - (aVal < bVal)) * (direction === 'asc' ? 1 : -1);
  });
};