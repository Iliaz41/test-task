import React from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
import { dateHelpers } from '../../utils/dateHelpers';

const WorkoutCalendar = ({ selectedDate, onDateChange, exercisesByDate }) => {
    const tileClassName = ({ date, view }) => {
        if (view === 'month') {
            const dateStr = dateHelpers.formatDate(date);
            
            if (exercisesByDate[dateStr] && exercisesByDate[dateStr].length > 0) {
                return 'has-exercises';
            }
            
            if (dateStr === dateHelpers.getTodayString()) {
                return 'today';
            }
        }
        return null;
    };

    const tileContent = ({ date, view }) => {
        if (view === 'month') {
            const dateStr = dateHelpers.formatDate(date);
            const count = exercisesByDate[dateStr]?.length;
            
            if (count) {
                return (
                    <div className="absolute bottom-0 left-0 right-0">
                        <div className="bg-green-500 text-white text-xs rounded-full w-5 h-5 flex items-center justify-center mx-auto">
                            {count}
                        </div>
                    </div>
                );
            }
        }
        return null;
    };

    return (
        <div className="bg-white rounded-lg shadow-md p-4">
            <style>{`
                .react-calendar {
                    border: none;
                    width: 100%;
                    font-family: inherit;
                }
                .react-calendar__tile {
                    position: relative;
                    padding: 1em 0.5em;
                }
                .react-calendar__tile.today {
                    background-color: #e6f3ff;
                    font-weight: bold;
                    color: #0066cc;
                }
                .react-calendar__tile.has-exercises {
                    background-color: #f0fff0;
                }
                .react-calendar__tile:enabled:hover {
                    background-color: #e6e6e6;
                }
                .react-calendar__tile--active {
                    background-color: #0066cc !important;
                    color: white !important;
                }
            `}</style>
            
            <Calendar
                onChange={onDateChange}
                value={selectedDate}
                tileClassName={tileClassName}
                tileContent={tileContent}
                locale="en-EN"
            />
            
            <div className="flex justify-between mt-4 text-sm text-gray-600">
                <div className="flex items-center gap-2">
                    <div className="w-3 h-3 bg-green-100 border border-green-300 rounded"></div>
                    <span>There are exercises</span>
                </div>
                <div className="flex items-center gap-2">
                    <div className="w-3 h-3 bg-blue-100 border border-blue-300 rounded"></div>
                    <span>Today</span>
                </div>
            </div>
        </div>
    );
};

export default WorkoutCalendar;