import React from 'react';
import { formatDuration } from '../../utils/formatters';
import { getSortIndicator } from '../../utils/sortUtils';
import { FaChevronDown, FaChevronUp } from 'react-icons/fa';
import CorrelatedEntriesTable from './CorrelatedEntriesTable';

const COLUMNS = [
  { key: 'userEmail', label: 'User Email' },
  { key: 'organization', label: 'Organization' },
  { key: 'startDate', label: 'Start Date' },
  { key: 'duration', label: 'Duration' },
  { key: 'entries', label: 'Entries' }
];

const AuditTable = ({ 
  logs, 
  organizations, 
  expandedRows, 
  onExpandRow, 
  sortConfig,
  onSort 
}) => (
  <table>
    <thead>
      <tr>
        {COLUMNS.map(({ key, label }) => (
          <th key={key} onClick={() => onSort(key)}>
            {label} {getSortIndicator(key, sortConfig)}
          </th>
        ))}
        <th></th>
      </tr>
    </thead>
    <tbody>
      {logs.map((log, index) => (
        <React.Fragment key={index}>
          <tr>
            <td>{log.userEmail || '-'}</td>
            <td>{organizations[log.organizationId] || '-'}</td>
            <td>  {new Date(log.startDate + 'Z').toLocaleString('en-GB', {
              year: 'numeric',
              month: '2-digit',
              day: '2-digit',
              hour: '2-digit',
              minute: '2-digit',
              second: '2-digit',
              hour12: false
            })}
            </td>
            <td>{formatDuration(log.duration)}</td>
            <td><span className="count-badge">{log.entries?.length || 0}</span></td>
            <td>
              {log.entries?.length > 0 && (
                <button onClick={() => onExpandRow(index)}>
                  {expandedRows[index] ? <FaChevronUp /> : <FaChevronDown />}
                </button>
              )}
            </td>
          </tr>
          {expandedRows[index] && (
            <tr>
              <td colSpan="6">
                <CorrelatedEntriesTable entries={log.entries} />
              </td>
            </tr>
          )}
        </React.Fragment>
      ))}
    </tbody>
  </table>
);

export default AuditTable;