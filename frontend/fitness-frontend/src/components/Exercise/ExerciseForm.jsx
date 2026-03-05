import React, { useState } from 'react';
import { FaPlus } from 'react-icons/fa';

const ExerciseForm = ({ onSubmit, units, statuses, selectedDate, userId }) => {
    const [formData, setFormData] = useState({
        name: '',
        description: '',
        repetitions: 10,
        approaches: 3,
        measurementUnitId: units[0]?.id || 1,
        statusId: statuses.find(s => s.name === 'Not started')?.id || 1
    });

    const handleSubmit = (e) => {
        e.preventDefault();
        
        const exerciseData = {
            ...formData,
            userId: userId,
        };
        
        onSubmit(exerciseData);
        
        setFormData({
            name: '',
            description: '',
            repetitions: 10,
            approaches: 3,
            measurementUnitId: units[0]?.id || 1,
            statusId: statuses.find(s => s.name === 'Not started')?.id || 1
        });
    };

    return (
        <form onSubmit={handleSubmit} className="bg-white rounded-lg shadow-md p-6">
            <h3 className="text-xl font-semibold mb-4">Add an exercise to {selectedDate}</h3>
            
            <div className="space-y-4">
                <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                        Name of the exercise *
                    </label>
                    <input
                        type="text"
                        required
                        value={formData.name}
                        onChange={(e) => setFormData({...formData, name: e.target.value})}
                        className="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                        placeholder="For example: run"
                    />
                </div>
                
                <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                        Description
                    </label>
                    <textarea
                        value={formData.description}
                        onChange={(e) => setFormData({...formData, description: e.target.value})}
                        className="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                        placeholder="Description of the exercise..."
                        rows="2"
                    />
                </div>
                
                <div className="grid grid-cols-2 gap-4">
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Number of repetitions
                        </label>
                        <input
                            type="number"
                            required
                            min="1"
                            value={formData.repetitions}
                            onChange={(e) => setFormData({...formData, repetitions: parseInt(e.target.value)})}
                            className="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                        />
                    </div>
                    
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Number of approaches
                        </label>
                        <input
                            type="number"
                            required
                            min="1"
                            value={formData.approaches}
                            onChange={(e) => setFormData({...formData, approaches: parseInt(e.target.value)})}
                            className="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                        />
                    </div>
                </div>
                
                <div className="grid grid-cols-2 gap-4">
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Unit of measurement
                        </label>
                        <select
                            value={formData.measurementUnitId}
                            onChange={(e) => setFormData({...formData, measurementUnitId: parseInt(e.target.value)})}
                            className="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                        >
                            {units.map(unit => (
                                <option key={unit.id} value={unit.id}>{unit.name}</option>
                            ))}
                        </select>
                    </div>
                    
                    <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                            Status
                        </label>
                        <select
                            value={formData.statusId}
                            onChange={(e) => setFormData({...formData, statusId: parseInt(e.target.value)})}
                            className="w-full p-2 border rounded focus:ring-2 focus:ring-blue-500"
                        >
                            {statuses.map(status => (
                                <option key={status.id} value={status.id}>{status.name}</option>
                            ))}
                        </select>
                    </div>
                </div>
                
                <button
                    type="submit"
                    className="w-full bg-blue-500 text-white py-2 px-4 rounded hover:bg-blue-600 transition-colors flex items-center justify-center gap-2"
                >
                    <FaPlus /> Add an exercise
                </button>
            </div>
        </form>
    );
};

export default ExerciseForm;