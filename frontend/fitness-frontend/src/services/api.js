import axios from 'axios';

const API_URL = 'https://localhost:7053/api';

export const MEASUREMENT_UNITS = [
    { id: 1, name: 'repetitions', guid: '13508eab-a5cc-479c-b249-320beee069d0' },
    { id: 2, name: 'kg', guid: '15ad1fb4-ca3f-45c0-92fb-254eea0b8a47' },
    { id: 3, name: 'minutes', guid: '35b30414-e84c-4dca-841b-4870205a2094' },
    { id: 4, name: 'km', guid: '88f9502d-94ef-4013-b544-b56f050cee3c' }
];

export const STATUSES = [
    { id: 1, name: 'Not started', guid: '958a8511-f2dc-4c6f-b6bb-89f25014703f' },
    { id: 2, name: 'In progress', guid: '989188da-d963-423d-9984-1a438b2496ff' },
    { id: 3, name: 'Completed', guid: 'e5e1f114-1c75-44a8-b6fc-62f8c4f783a5' },
    { id: 4, name: 'Skipped', guid: '32f0cfac-9bd3-459d-b000-8179c1df2be2' }
];

const api = axios.create({
    baseURL: API_URL,
    headers: {
        'Content-Type': 'application/json',
    },
});

api.interceptors.response.use(
    response => response.data,
    error => {
        console.error('API Error:', error.response?.data || error.message);
        throw error.response?.data || error.message;
    }
);

export const exerciseApi = {
    getAllExercises: () => 
        api.get('/Exercise'),
    
    getExerciseById: (id) => 
        api.get(`/Exercise/${id}`),
    
    getUserExercisesForDay: async (userId, day) => {
        const formattedDay = day.includes('T') ? day : `${day}T00:00:00`;
        
        try {
            const exercises = await api.get(`/Exercise/${userId}/exercises`, { 
                params: { day: formattedDay }
            });
            
            return exercises.map(exercise => ({
                ...exercise,
                measurementUnit: exercise.measurementUnit || 
                    MEASUREMENT_UNITS.find(u => u.id === exercise.measurementUnitId),
                status: exercise.status || 
                    STATUSES.find(s => s.id === exercise.statusId)
            }));
        } catch (error) {
            console.error('Error fetching exercises:', error);
            throw error;
        }
    },
    
    createExercise: (exerciseData) => 
        api.post('/Exercise', {
            name: exerciseData.name,
            description: exerciseData.description || '',
            repetitions: exerciseData.repetitions,
            approaches: exerciseData.approaches,
            userId: exerciseData.userId,
            calendarId: exerciseData.calendarId,
            measurementUnitId: exerciseData.measurementUnitId,
            statusId: exerciseData.statusId
        }),
    
    updateExercise: (id, exerciseData) => 
        api.put(`/Exercise/${id}`, {
            id: id,
            name: exerciseData.name,
            description: exerciseData.description || '',
            repetitions: exerciseData.repetitions,
            approaches: exerciseData.approaches,
            userId: exerciseData.userId,
            calendarId: exerciseData.calendarId,
            measurementUnitId: exerciseData.measurementUnitId,
            statusId: exerciseData.statusId,
            measurementUnit: null,
            status: null
        }),
    
    deleteExercise: (id) => 
        api.delete(`/Exercise/${id}`),
};

export const calendarApi = {
    getAllDays: () => 
        api.get('/Calendar'),
    
    getDayById: (id) => 
        api.get(`/Calendar/${id}`),
    
    createDay: (day) => {
        const formattedDay = day.includes('T') ? day : `${day}T00:00:00`;
        return api.post('/Calendar', { 
            day: formattedDay
        });
    },
    
    updateDay: (id, day) => {
        const formattedDay = day.includes('T') ? day : `${day}T00:00:00`;
        return api.put(`/Calendar/${id}`, {
            id: id,
            day: formattedDay
        });
    },
    
    deleteDay: (id) => 
        api.delete(`/Calendar/${id}`),
    
    findDayByDate: async (date) => {
        const days = await api.get('/Calendar');
        const dateStr = date.split('T')[0];
        return days.find(d => d.day.split('T')[0] === dateStr);
    },
    
    getOrCreateDay: async (date) => {
        const formattedDate = date.includes('T') ? date : `${date}T00:00:00`;
        const existingDay = await calendarApi.findDayByDate(formattedDate);
        
        if (existingDay) {
            return existingDay;
        }
        
        return await calendarApi.createDay(formattedDate);
    }
};

export const helpers = {
    getUnitName: (unitId) => {
        const unit = MEASUREMENT_UNITS.find(u => u.id === unitId);
        return unit ? unit.name : 'unknown';
    },
    
    getStatusName: (statusId) => {
        const status = STATUSES.find(s => s.id === statusId);
        return status ? status.name : 'unknown';
    },
    
    getStatusColor: (statusName) => {
        switch(statusName?.toLowerCase()) {
            case 'not started': return 'bg-red-100 border-red-300';
            case 'in progress': return 'bg-yellow-100 border-yellow-300';
            case 'completed': return 'bg-green-100 border-green-300';
            case 'skipped': return 'bg-gray-100 border-gray-300';
            default: return 'bg-gray-100 border-gray-300';
        }
    },
    
    getUnits: () => MEASUREMENT_UNITS,
    
    getStatuses: () => STATUSES
};

export default api;