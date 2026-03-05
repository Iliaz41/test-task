import React, { useState } from 'react';
import { FaEdit, FaTrash, FaSave, FaTimes } from 'react-icons/fa';

const ExerciseCard = ({ exercise, onUpdate, onDelete, units, statuses }) => {
    const [isEditing, setIsEditing] = useState(false);
    const [editedExercise, setEditedExercise] = useState({
        name: exercise.name,
        description: exercise.description || '',
        repetitions: exercise.repetitions,
        approaches: exercise.approaches,
        measurementUnitId: exercise.measurementUnitId,
        statusId: exercise.statusId
    });

    const handleSave = () => {
        onUpdate(exercise.id, {
            ...editedExercise,
            userId: exercise.userId,
            calendarId: exercise.calendarId
        });
        setIsEditing(false);
    };

    const getStatusColor = (statusName) => {
        switch(statusName?.toLowerCase()) {
            case 'not started': return 'bg-red-100 border-red-300';
            case 'in progress': return 'bg-yellow-100 border-yellow-300';
            case 'completed': return 'bg-green-100 border-green-300';
            case 'skipped': return 'bg-gray-100 border-gray-300';
            default: return 'bg-gray-100 border-gray-300';
        }
    };

    if (isEditing) {
        return (
            <div className={`border rounded-lg p-4 mb-3 ${getStatusColor(exercise.status?.name)}`}>
                <div className="space-y-3">
                    <input
                        type="text"
                        value={editedExercise.name}
                        onChange={(e) => setEditedExercise({...editedExercise, name: e.target.value})}
                        className="w-full p-2 border rounded"
                        placeholder="Name of the exercise"
                    />
                    
                    <textarea
                        value={editedExercise.description}
                        onChange={(e) => setEditedExercise({...editedExercise, description: e.target.value})}
                        className="w-full p-2 border rounded"
                        placeholder="Description"
                        rows="2"
                    />
                    
                    <div className="flex gap-2">
                        <input
                            type="number"
                            value={editedExercise.repetitions}
                            onChange={(e) => setEditedExercise({...editedExercise, repetitions: parseInt(e.target.value)})}
                            className="w-24 p-2 border rounded"
                            placeholder="Repeats"
                        />
                        <input
                            type="number"
                            value={editedExercise.approaches}
                            onChange={(e) => setEditedExercise({...editedExercise, approaches: parseInt(e.target.value)})}
                            className="w-24 p-2 border rounded"
                            placeholder="Approaches"
                        />
                        
                        <select
                            value={editedExercise.measurementUnitId}
                            onChange={(e) => setEditedExercise({...editedExercise, measurementUnitId: parseInt(e.target.value)})}
                            className="p-2 border rounded"
                        >
                            {units.map(unit => (
                                <option key={unit.id} value={unit.id}>{unit.name}</option>
                            ))}
                        </select>
                    </div>
                    
                    <select
                        value={editedExercise.statusId}
                        onChange={(e) => setEditedExercise({...editedExercise, statusId: parseInt(e.target.value)})}
                        className="w-full p-2 border rounded"
                    >
                        {statuses.map(status => (
                            <option key={status.id} value={status.id}>{status.name}</option>
                        ))}
                    </select>
                    
                    <div className="flex justify-end gap-2">
                        <button
                            onClick={handleSave}
                            className="bg-green-500 text-white px-3 py-1 rounded hover:bg-green-600 flex items-center gap-1"
                        >
                            <FaSave /> Save
                        </button>
                        <button
                            onClick={() => setIsEditing(false)}
                            className="bg-gray-500 text-white px-3 py-1 rounded hover:bg-gray-600 flex items-center gap-1"
                        >
                            <FaTimes /> Cancel
                        </button>
                    </div>
                </div>
            </div>
        );
    }

    return (
        <div className={`border rounded-lg p-4 mb-3 shadow-sm hover:shadow-md transition-shadow ${getStatusColor(exercise.status?.name)}`}>
            <div className="flex justify-between items-start">
                <div className="flex-grow">
                    <h3 className="text-lg font-semibold">{exercise.name}</h3>
                    {exercise.description && (
                        <p className="text-gray-600 text-sm mt-1">{exercise.description}</p>
                    )}
                    <div className="flex items-center gap-4 mt-2 text-sm">
                        <span className="bg-white px-2 py-1 rounded">
                            {exercise.repetitions} x {exercise.approaches} {exercise.measurementUnit?.name}
                        </span>
                        <span className="bg-white px-2 py-1 rounded">
                            Status: {exercise.status?.name}
                        </span>
                    </div>
                </div>
                
                <div className="flex gap-2">
                    <button
                        onClick={() => setIsEditing(true)}
                        className="text-blue-500 hover:text-blue-700"
                        title="Edit"
                    >
                        <FaEdit size={18} />
                    </button>
                    <button
                        onClick={() => onDelete(exercise.id)}
                        className="text-red-500 hover:text-red-700"
                        title="Remove"
                    >
                        <FaTrash size={18} />
                    </button>
                </div>
            </div>
        </div>
    );
};

export default ExerciseCard;