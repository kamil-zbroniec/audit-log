import React from 'react';

const Pagination = ({ 
  pageNumber, 
  totalRecords, 
  pageSize, 
  onPageChange, 
  onPageSizeChange 
}) => {
  const totalPages = Math.ceil(totalRecords / pageSize);
  
  return (
    <div className="pagination-container">
      <div className="pagination-controls">
        <button 
          onClick={() => onPageChange(pageNumber - 1)}
          disabled={pageNumber === 1}
        >
          &lt;
        </button>
        <span>Page {pageNumber} of {totalPages}</span>
        <button 
          onClick={() => onPageChange(pageNumber + 1)}
          disabled={pageNumber * pageSize >= totalRecords}
        >
          &gt;
        </button>
      </div>
      <select 
        className="input-common"
        style={{ width: 'auto' }}
        value={pageSize} 
        onChange={e => onPageSizeChange(Number(e.target.value))}
      >
        {[5, 10, 15, 20, 50].map(size => (
          <option key={size} value={size}>{size} rows</option>
        ))}
      </select>
    </div>
  );
};

export default Pagination;