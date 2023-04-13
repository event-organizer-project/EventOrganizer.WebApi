import { useState, useEffect } from 'react';
import { Box, Typography } from '@mui/material';
import EventsGrid from 'components/EventsGrid/EventsGrid'
import eventRequestService from 'services/eventRequestService'

export default function AllEventsPage() {

    const itemsPerPageCount = 9;

    const [events, setEvents] = useState(null);

    useEffect(() => {
        eventRequestService.getList(100, 0)
            .then(result => {
                setEvents(result);
            });
    }, [])

    return (
        <Box>
            <Typography variant='h6' gutterBottom padding='5px 0'>
                There will be a page with an event search panel and tools for creating new events.
            </Typography>

            {events && <EventsGrid events={events} itemsPerPageCount={itemsPerPageCount} height='70vh'/>}
        </Box>
    )
}