import React from 'react';
import ExerciseCard from './ExerciseCard';

const ExerciseList = ({ exercises, onUpdate, onDelete, units, statuses }) => {
    if (!exercises || exercises.length === 0) {
        return (
            <div className="text-center py-8 bg-gray-100 rounded-lg">
                <p className="text-gray-500">No exercise for this day</p>
                <p className="text-sm text-gray-400 mt-2">Add a new exercise using the form below</p>
            </div>
        );
    }

    return (
        <div className="space-y-3">
            {exercises.map(exercise => (
                <ExerciseCard
                    key={exercise.id}
                    exercise={exercise}
                    onUpdate={onUpdate}
                    onDelete={onDelete}
                    units={units}
                    statuses={statuses}
                />
            ))}
        </div>
    );
};

export default ExerciseList;