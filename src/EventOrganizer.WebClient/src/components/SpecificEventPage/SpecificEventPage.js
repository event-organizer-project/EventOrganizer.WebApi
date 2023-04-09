import { useParams } from "react-router-dom";
import React, { useEffect, useState } from 'react';
import eventRequestService from '../../services/eventRequestService'
import SpecificEventPageView from './SpecificEventPageView'
import SpecificEventPageUpsert from './SpecificEventPageUpsert'

export default function SpecificEventPage() {

    let { id } = useParams();

    const [event, setEvent] = useState(null);

    const [upsertMode, setUpsertMode] = useState(false);

    useEffect(() => {
        if (id == 0) {
            setUpsertMode(true);
        }
        else {
            eventRequestService.get(id)
                .then(result => setEvent(result));
        }
    }, [])

    return upsertMode 
        ? <SpecificEventPageUpsert event={event} toViewMode={() => setUpsertMode(false)} setEvent={setEvent} /> 
        : <SpecificEventPageView event={event} toUpdateMode={() => setUpsertMode(true)} />
}
