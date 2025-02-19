import React, { useState, useEffect } from 'react';
import { fetchAuditLogs } from '../../api/auditLogApi';
import { fetchOrganizations } from '../../api/organizationApi';
import { sortData } from '../../utils/sortUtils';
import Filters from './Filters';
import AuditLogTable from './AuditLogTable';
import Pagination from './Pagination';

const AuditLogPage = () => {
  const [selectedOrg, setSelectedOrg] = useState("");
  const [auditLogs, setAuditLogs] = useState([]);
  const [filteredLogs, setFilteredLogs] = useState([]);
  const [expandedRows, setExpandedRows] = useState({});
  const [pageNumber, setPageNumber] = useState(1);
  const [pageSize, setPageSize] = useState(10);
  const [totalRecords, setTotalRecords] = useState(0);
  const [searchTerm, setSearchTerm] = useState("");
  const [isLoading, setIsLoading] = useState(false);
  const [startDate, setStartDate] = useState(null);
  const [endDate, setEndDate] = useState(null);
  const [organizations, setOrganizations] = useState({});
  const [sortConfig, setSortConfig] = useState({ key: 'startDate', direction: 'desc' });

  useEffect(() => {
    const fetchOrgs = async () => {
      try {
        const orgs = await fetchOrganizations();
        const orgsMap = orgs.reduce((acc, org) => ({
          ...acc,
          [org.id]: org.name || org.id
        }), {});
        setOrganizations(orgsMap);
      } catch (error) {
        console.error('Error fetching organizations:', error);
      }
    };
    fetchOrgs();
  }, []);

  useEffect(() => {
    const fetchData = async () => {
      setIsLoading(true);
      try {
        const data = await fetchAuditLogs(
          selectedOrg, 
          startDate?.toISOString(),
          endDate?.toISOString(),
          pageNumber, 
          pageSize
        );
        setAuditLogs(data.items || []);
        setFilteredLogs(data.items || []);
        setTotalRecords(data.totalRecordCount || 0);
      } catch (error) {
        console.error('Error fetching audit logs:', error);
      } finally {
        setIsLoading(false);
      }
    };
    fetchData();
  }, [selectedOrg, pageNumber, pageSize, startDate, endDate]);

  useEffect(() => {
    const term = searchTerm.toLowerCase();
    const filtered = auditLogs.filter(log => 
      !term || 
      log.userEmail?.toLowerCase().includes(term) ||
      organizations[log.organizationId]?.toLowerCase().includes(term) ||
      log.type?.toLowerCase().includes(term)
    );
    setFilteredLogs(filtered);
  }, [searchTerm, auditLogs, organizations]);

  const handleSort = (key) => {
    const direction = sortConfig.key === key && sortConfig.direction === 'asc' ? 'desc' : 'asc';
    setSortConfig({ key, direction });
    const sorted = sortData(filteredLogs, key, direction, organizations);
    setFilteredLogs(sorted);
  };

  const handlePageSizeChange = (newSize) => {
    setPageSize(newSize);
    setPageNumber(1);
  };

  const handleDateChange = (type, date) => {
    if (type === 'start') {
      setStartDate(date);
    } else {
      setEndDate(date);
    }
  };

  const handleOrgChange = (orgId) => {
    setSelectedOrg(orgId);
    setPageNumber(1);
  };

  const toggleRow = (index) => {
    setExpandedRows(prev => ({
      ...prev,
      [index]: !prev[index]
    }));
  };

  return (
    <div className="table-container">
      <Filters
        searchTerm={searchTerm}
        onSearchChange={setSearchTerm}
        startDate={startDate}
        endDate={endDate}
        onDateChange={handleDateChange}
        selectedOrg={selectedOrg}
        organizations={organizations}
        onOrgChange={handleOrgChange}
      />

      {isLoading ? (
        <div className="loading-overlay">Loading...</div>
      ) : filteredLogs.length === 0 ? (
        <div className="no-data-message">No data to display</div>
      ) : (
        <>
          <AuditLogTable
            logs={filteredLogs}
            organizations={organizations}
            expandedRows={expandedRows}
            onExpandRow={toggleRow}
            sortConfig={sortConfig}
            onSort={handleSort}
          />
          <Pagination
            pageNumber={pageNumber}
            totalRecords={totalRecords}
            pageSize={pageSize}
            onPageChange={setPageNumber}
            onPageSizeChange={handlePageSizeChange}
          />
        </>
      )}
    </div>
  );
};

export default AuditLogPage;