import { useLoaderData } from "react-router-dom";

export async function loader({ params }) {
    return fetch(`/api/event/${params.id}`)
    .then(response => response.json())
}

export default function SpecificEventPage () {

    const event = useLoaderData();

    return (
        <main>
            <h3>Specific event page</h3>
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
                {event.eventTags ? event.eventTags.map(tag =>
                    <label key={tag}>#{tag} </label>
                    ) : null}
            </div>
        </main>
    )
}