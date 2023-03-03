import { useParams } from "react-router-dom";
import React, { useEffect, useState } from 'react';
import eventRequestService from '../../services/eventRequestService'


export default function SpecificEventPage () {

    let { id } = useParams();

    const [ event, setEvent ] = useState({});

    useEffect(() => {
        console.log(id)
        eventRequestService.get(id)
            .then(result => setEvent(result));
    }, [])

    return event && ( 
        <div>
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
        </div>
    )
}