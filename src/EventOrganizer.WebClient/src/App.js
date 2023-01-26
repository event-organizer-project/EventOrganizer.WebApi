import { useEffect, useState } from "react";

const App = () => {

    const [event, setEvent] = useState({})
    const [eventId, setEventId] = useState({});

    const getEvent = (id) => fetch(`api/event/${id}`)
        .then(response => { return response.json() })
        .then(responseJson => {
            setEvent(responseJson)
        });

    const onSubmit = (e) => {
        e.preventDefault();
        getEvent(eventId);
    }

    return (
        <div className="container">
            <form>
                <h3>Get specific event</h3>

                <div>
                    <label>Event Id:</label>
                    <input
                        type="number"
                        placeholder="Input event ID"
                        name="eventId"
                        value={eventId}
                        onChange={(e) => setEventId(e.target.value)}
                    />
                </div>

                <button
                    type="submit"
                    onClick={onSubmit} >
                    Search
                </button>
            </form>
            <div>
                <h4>Event Title:</h4>
                <label>{event.title}</label>

                <h4>Event Description:</h4>
                <label>{event.description}</label>

                <h4>Start Date:</h4>
                <label>{event.startDate} {event.startTime}</label>

                <h4>End Date:</h4>
                <label>{event.endDate} {event.endTime}</label>

                <h4>Tags:</h4>
                {event.eventTags.map(tag =>
                    <label>#{tag} </label>
                    )}
            </div>
        </div>
    )
}

export default App;