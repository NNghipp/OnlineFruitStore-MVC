/**
 * MyStore Custom JavaScript Library
 * Các hàm tiện ích tùy chỉnh
 */

var MyStoreUtils = (function () {
    'use strict';

    // Format số tiền VND
    function formatCurrency(amount) {
        return new Intl.NumberFormat('vi-VN', {
            style: 'currency',
            currency: 'VND'
        }).format(amount);
    }

    // Format ngày tháng tiếng Việt
    function formatDate(date) {
        return new Intl.DateTimeFormat('vi-VN', {
            day: '2-digit',
            month: '2-digit',
            year: 'numeric'
        }).format(new Date(date));
    }

    // Debounce function - Tránh gọi hàm liên tục
    function debounce(func, wait) {
        var timeout;
        return function () {
            var context = this, args = arguments;
            clearTimeout(timeout);
            timeout = setTimeout(function () {
                func.apply(context, args);
            }, wait);
        };
    }

    // Show loading spinner
    function showLoading(element) {
        element.innerHTML = '<div class="spinner-border" role="status"><span class="visually-hidden">Loading...</span></div>';
    }

    // Hide loading spinner
    function hideLoading(element, content) {
        element.innerHTML = content;
    }

    // Public API
    return {
        formatCurrency: formatCurrency,
        formatDate: formatDate,
        debounce: debounce,
        showLoading: showLoading,
        hideLoading: hideLoading
    };
})();

// Example usage:
// console.log(MyStoreUtils.formatCurrency(1500000)); // "1.500.000 ₫"
// console.log(MyStoreUtils.formatDate('2025-01-15')); // "15/01/2025"
