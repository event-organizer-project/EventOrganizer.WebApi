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
                <h4>Event ID:</h4>
                <label>{event.id}</label>

                <h4>Event Title:</h4>
                <label>{event.title}</label>

                <h4>Event Description</h4>
                <label>{event.description}</label>

            </div>
        </div>
    )
}

export default App;