const ENTITY_TYPE_MAPPING = {
  'Unknown': 'Unknown',
  'ContractHeaderEntity': 'Contract Header',
  'AnnexHeaderEntity': 'Annex Header',
  'AnnexChangeEntity': 'Annex Change',
  'FileEntity': 'File',
  'InvoiceEntity': 'Invoice',
  'PaymentScheduleEntity': 'Payment Schedule',
  'ContractFundingEntity': 'Contract Funding'
};

export const formatEntityType = (entityType) => {
  if (!entityType) return 'N/A';
  return ENTITY_TYPE_MAPPING[entityType] || entityType;
};

export const formatDuration = (timespan) => {
  if (!timespan) return 'N/A';
  const [time, milliseconds] = timespan.split('.');
  if (!milliseconds) return time;
  return `${time}.${milliseconds.substring(0, 3)}`;
};

export const getEntityTypeColor = (entityType) => {
  if (!entityType) return '#3b82f6';
  
  let hash = 0;
  for (let i = 0; i < entityType.length; i++) {
    hash = entityType.charCodeAt(i) + ((hash << 5) - hash);
  }
  
  const colors = [
    '#3b82f6', // blue
    '#10b981', // green
    '#8b5cf6', // purple
    '#f59e0b', // amber
    '#6366f1', // indigo
    '#ec4899', // pink
    '#14b8a6', // teal
    '#f97316', // orange
    '#6b7280', // gray
    '#ef4444'  // red
  ];
  
  return colors[Math.abs(hash) % colors.length];
};