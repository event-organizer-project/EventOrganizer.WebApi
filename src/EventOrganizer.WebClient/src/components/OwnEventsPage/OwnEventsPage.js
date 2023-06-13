import { useState, useEffect } from 'react';
import { Button } from '@mui/material';
import { Link } from 'react-router-dom';
import { Box, Typography } from '@mui/material';
import Tooltip from '@mui/material/Tooltip';
import IconButton from '@mui/material/IconButton';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import EventsGrid from 'components/EventsGrid/EventsGrid'
import eventRequestService from 'services/eventRequestService'
import routes from 'constants/route-constants'

export default function OwnEventsPage() {

    const [ownEvents, setOwnEvents] = useState(null);

    useEffect(() => {
        eventRequestService.getOwnEvents()
            .then(result => {
                setOwnEvents(result);
            });
    }, [])

    return ownEvents && (
        <Box>
            <Box display='flex' margin='auto auto'>
                <Typography variant='h5' gutterBottom padding='6px 2px'>
                    Created events
                </Typography>
                <Button component={Link} to={`${routes.events}/0`} variant="contained" color="primary">
                    Create New Event
                </Button>
                <Tooltip onClick={null/*handleClick*/} margin='auto'>
                <IconButton>
                    <AddCircleOutlineIcon />
                </IconButton>
            </Tooltip>
            </Box>

            <EventsGrid events={ownEvents.createdEvents} itemsPerPageCount={3}/>
            <Typography variant='h5' gutterBottom margin='auto auto'>Joined  events</Typography>
            <EventsGrid events={ownEvents.joinedEvents} itemsPerPageCount={3}/>
        </Box>
    )
}