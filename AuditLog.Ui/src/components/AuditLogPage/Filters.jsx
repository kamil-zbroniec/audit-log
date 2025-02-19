import React from 'react';
import DatePicker from 'react-datepicker';
import "react-datepicker/dist/react-datepicker.css";

const Filters = ({ 
  searchTerm, 
  onSearchChange, 
  startDate, 
  endDate, 
  onDateChange,
  selectedOrg,
  organizations,
  onOrgChange 
}) => (
  <div className="filter-container">
    <input
      type="text"
      placeholder="Search..."
      value={searchTerm}
      onChange={e => onSearchChange(e.target.value)}
      className="input-common"
    />
    <div className="right-filters">
      <div className="date-range-container">
        <div className="date-picker-wrapper">
          <DatePicker
            selected={startDate}
            onChange={date => onDateChange('start', date)}
            selectsStart
            startDate={startDate}
            endDate={endDate}
            dateFormat="dd/MM/yyyy"
            placeholderText="Start date"
            className="input-common"
            isClearable
            utcOffset={0}
            timeZone="UTC"
          />
        </div>
        <div className="date-picker-wrapper">
          <DatePicker
            selected={endDate}
            onChange={date => onDateChange('end', date)}
            selectsEnd
            startDate={startDate}
            endDate={endDate}
            minDate={startDate}
            dateFormat="dd/MM/yyyy"
            placeholderText="End date"
            className="input-common"
            isClearable
            utcOffset={0}
            timeZone="UTC"
          />
        </div>
      </div>
      <select 
        className="input-common"
        value={selectedOrg} 
        onChange={e => onOrgChange(e.target.value)}
      >
        <option value="">Select Organization</option>
        {Object.entries(organizations).map(([id, name]) => (
          <option key={id} value={id}>{name}</option>
        ))}
      </select>
    </div>
  </div>
);

export default Filters;