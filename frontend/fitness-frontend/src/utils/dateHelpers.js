export const dateHelpers = {
        formatDate: (date) => {
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const day = String(date.getDate()).padStart(2, '0');
        return `${year}-${month}-${day}`;
    },
    
    parseDate: (dateStr) => {
        const [year, month, day] = dateStr.split('-').map(Number);
        return new Date(year, month - 1, day);
    },
    
    isToday: (dateStr) => {
        const today = new Date();
        const todayStr = dateHelpers.formatDate(today);
        return dateStr === todayStr;
    },
    
    getTodayString: () => {
        return dateHelpers.formatDate(new Date());
    },
    
    formatDisplayDate: (date) => {
        return date.toLocaleDateString('en-En', {
            year: 'numeric',
            month: 'long',
            day: 'numeric'
        });
    }
};