/**
 * MyStore - Main JavaScript File
 * Site-wide JavaScript functionality
 */

// ============================================================================
// Document Ready
// ============================================================================
(function () {
    'use strict';

    // Initialize when DOM is ready
    document.addEventListener('DOMContentLoaded', function () {
        console.log('MyStore initialized');
        initializeComponents();
    });

    // ========================================================================
    // Component Initialization
    // ========================================================================
    function initializeComponents() {
        initTooltips();
        initAlertDismiss();
        initConfirmDelete();
        initSearchFilter();
    }

    // ========================================================================
    // Bootstrap Tooltips
    // ========================================================================
    function initTooltips() {
        var tooltipTriggerList = [].slice.call(
            document.querySelectorAll('[data-bs-toggle="tooltip"]')
        );
        tooltipTriggerList.forEach(function (el) {
            new bootstrap.Tooltip(el);
        });
    }

    // ========================================================================
    // Auto-dismiss Alerts
    // ========================================================================
    function initAlertDismiss() {
        var alerts = document.querySelectorAll('.alert-dismissible');
        alerts.forEach(function (alert) {
            setTimeout(function () {
                var bsAlert = bootstrap.Alert.getOrCreateInstance(alert);
                bsAlert.close();
            }, 5000); // Auto-dismiss after 5 seconds
        });
    }

    // ========================================================================
    // Confirm Delete
    // ========================================================================
    function initConfirmDelete() {
        var deleteButtons = document.querySelectorAll('.btn-delete-confirm');
        deleteButtons.forEach(function (btn) {
            btn.addEventListener('click', function (e) {
                if (!confirm('Bạn có chắc chắn muốn xóa không?')) {
                    e.preventDefault();
                }
            });
        });
    }

    // ========================================================================
    // Table Search/Filter
    // ========================================================================
    function initSearchFilter() {
        var searchInput = document.getElementById('tableSearch');
        if (searchInput) {
            searchInput.addEventListener('keyup', function () {
                var filter = this.value.toLowerCase();
                var table = document.querySelector('.table tbody');
                if (!table) return;

                var rows = table.querySelectorAll('tr');
                rows.forEach(function (row) {
                    var text = row.textContent.toLowerCase();
                    row.style.display = text.indexOf(filter) > -1 ? '' : 'none';
                });
            });
        }
    }

})();
