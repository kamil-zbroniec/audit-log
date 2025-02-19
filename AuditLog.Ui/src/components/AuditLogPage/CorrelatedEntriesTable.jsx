import React from 'react';
import { TypeBadge, EntityBadge } from '../common/Badge';
import { formatEntityType } from '../../utils/formatters';

const CorrelatedEntriesTable = ({ entries }) => (
  <table>
    <thead>
      <tr>
        <th>Entity Type</th>
        <th>Entity ID</th>
        <th>Number</th>
        <th>Operation Type</th>
      </tr>
    </thead>
    <tbody>
      {entries.map((entry, i) => (
        <tr key={i}>
          <td>
            <EntityBadge entityType={formatEntityType(entry.entity.entityType)} />
          </td>
          <td>{entry.entity.id}</td>
          <td>
            {entry.entity.entityType === 'ContractHeaderEntity' 
              ? entry.entity.number 
              : '-'}
          </td>
          <td><TypeBadge type={entry.operationType} /></td>
        </tr>
      ))}
    </tbody>
  </table>
);

export default CorrelatedEntriesTable;