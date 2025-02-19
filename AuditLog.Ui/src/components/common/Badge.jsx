import React from 'react';
import { getEntityTypeColor } from '../../utils/formatters';

export const Badge = ({ type, className, children, style }) => (
  <span className={`badge ${className}`} style={style}>
    {children}
  </span>
);

export const TypeBadge = ({ type }) => (
  <Badge className={`badge-${type.toLowerCase()}`}>
    {type}
  </Badge>
);

export const EntityBadge = ({ entityType }) => (
  <Badge 
    className="badge-entity" 
    style={{ backgroundColor: getEntityTypeColor(entityType) }}
  >
    {entityType}
  </Badge>
);

export const CountBadge = ({ count }) => (
  <span className="count-badge">{count}</span>
);