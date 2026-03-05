import React, { useState, useEffect } from 'react';
import WorkoutCalendar from './components/Calendar/WorkoutCalendar';
import ExerciseList from './components/Exercise/ExerciseList';
import ExerciseForm from './components/Exercise/ExerciseForm';
import Loader from './components/UI/Loader';
import Alert from './components/UI/Alert';
import { exerciseApi, calendarApi, helpers } from './services/api';
import { dateHelpers } from './utils/dateHelpers';

function App() {
    // Cond
    const [selectedDate, setSelectedDate] = useState(new Date());
    const [exercises, setExercises] = useState([]);
    const [loading, setLoading] = useState(false);
    const [alert, setAlert] = useState(null);
    const [exercisesByDate, setExercisesByDate] = useState({});
    
    // MU, st
    const [units] = useState(helpers.getUnits());
    const [statuses] = useState(helpers.getStatuses());

    // const userID
    const userId = 1;

    useEffect(() => {
        loadExercises();
    }, [selectedDate]);

    const loadExercises = async () => {
        setLoading(true);
        try {
            const dateStr = dateHelpers.formatDate(selectedDate);
            console.log('Loading exercises for date:', dateStr); 
            const data = await exerciseApi.getUserExercisesForDay(userId, dateStr);
            setExercises(data);
            
            updateExercisesByDate(data, dateStr);
            
        } catch (error) {
            showAlert('Error loading exercises', 'error');
            console.error('Load exercises error:', error);
        } finally {
            setLoading(false);
        }
    };

    const updateExercisesByDate = (exercisesData, dateStr) => {
        setExercisesByDate(prev => ({
            ...prev,
            [dateStr]: exercisesData
        }));
    };

    const handleCreateExercise = async (exerciseData) => {
        try {
            const dateStr = dateHelpers.formatDate(selectedDate);
            console.log('Creating exercise for date:', dateStr); 
            
            const calendarDay = await calendarApi.getOrCreateDay(dateStr);
            console.log('Calendar day:', calendarDay); 
            
            await exerciseApi.createExercise({
                ...exerciseData,
                userId: userId,
                calendarId: calendarDay.id
            });
            
            await loadExercises();
            
            showAlert('Exercise added successfully', 'success');
        } catch (error) {
            console.error('Error creating exercise:', error);
            showAlert('Error when adding an exercise', 'error');
        }
    };

    const handleUpdateExercise = async (exerciseId, exerciseData) => {
        try {
            console.log('Updating exercise:', exerciseId, exerciseData); 
            
            await exerciseApi.updateExercise(exerciseId, {
                ...exerciseData,
                userId: userId,
                calendarId: exercises.find(e => e.id === exerciseId).calendarId
            });
            
            await loadExercises();
            
            showAlert('The exercise has been updated', 'success');
        } catch (error) {
            console.error('Error updating exercise:', error);
            showAlert('Error during the update', 'error');
        }
    };

    const handleDeleteExercise = async (exerciseId) => {
        if (!window.confirm('Are you sure you want to delete this exercise?')) {
            return;
        }

        try {
            console.log('Deleting exercise:', exerciseId); 
            
            await exerciseApi.deleteExercise(exerciseId);
            
            await loadExercises();
            
            showAlert('Exercise deleted', 'success');
        } catch (error) {
            console.error('Error deleting exercise:', error);
            showAlert('Error when deleting', 'error');
        }
    };

    const showAlert = (message, type = 'info') => {
        setAlert({ message, type });
        setTimeout(() => setAlert(null), 5000);
    };

    console.log('Selected date:', selectedDate);
    console.log('Formatted date:', dateHelpers.formatDate(selectedDate));

    return (
        <div className="min-h-screen bg-gray-50 py-8">
            <div className="container mx-auto px-4 max-w-6xl">
                <header className="mb-8">
                    <h1 className="text-3xl font-bold text-center text-gray-800">
                        🏋️‍♂️ Fitness Tracker
                    </h1>
                    <p className="text-center text-gray-600 mt-2">
                        {dateHelpers.formatDisplayDate(selectedDate)}
                    </p>
                </header>

                {alert && (
                    <Alert 
                        type={alert.type} 
                        message={alert.message} 
                        onClose={() => setAlert(null)}
                    />
                )}

                <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
                    {/* Calendar */}
                    <div className="lg:col-span-1">
                        <WorkoutCalendar
                            selectedDate={selectedDate}
                            onDateChange={setSelectedDate}
                            exercisesByDate={exercisesByDate}
                        />
                    </div>

                    {/* Exercise list and form */}
                    <div className="lg:col-span-2 space-y-6">
                        <div className="bg-white rounded-lg shadow-md p-6">
                            <h2 className="text-2xl font-semibold mb-4">
                                Exercises for {dateHelpers.formatDisplayDate(selectedDate)}
                            </h2>
                            
                            {loading ? (
                                <Loader />
                            ) : (
                                <ExerciseList
                                    exercises={exercises}
                                    onUpdate={handleUpdateExercise}
                                    onDelete={handleDeleteExercise}
                                    units={units}
                                    statuses={statuses}
                                />
                            )}
                        </div>

                        <ExerciseForm
                            onSubmit={handleCreateExercise}
                            units={units}
                            statuses={statuses}
                            selectedDate={dateHelpers.formatDisplayDate(selectedDate)}
                            userId={userId}
                        />
                    </div>
                </div>
            </div>
        </div>
    );
}

export default App;