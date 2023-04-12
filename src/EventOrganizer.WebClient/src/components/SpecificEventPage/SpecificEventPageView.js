import { Box, Grid, Typography } from '@mui/material';
import AccessTimeIcon from '@mui/icons-material/AccessTime';
import EventTagList from 'components/EventTagList/EventTagList'
import UserView from 'components/UserView/UserView'
import UserViewList from 'components/UserViewList/UserViewList'
import EventOptionsMenu from './EventOptionsMenu'
import { mapEnumToString } from 'mappers/recurrence-type-mapper'

export default function SpecificEventPageView({ event, setEvent, toUpdateMode }) {

    return event && (
        <Box>
            <Box display='flex'>
                <Typography variant='h5' gutterBottom padding='6px 2px'>
                    {event.title}
                </Typography>
                <EventOptionsMenu
                    toUpdateEvent={toUpdateMode}
                    event={event}
                    setEvent={setEvent}
                />
            </Box>
            <Grid container spacing={2} width='100vw'>
                <Grid item xs={12} sm={5} width='100%' >

                    <Typography variant="subtitle2">Description:</Typography>
                    <Typography>{event.description}</Typography>

                    <Typography variant="subtitle2">Date:</Typography>
                    <Typography>{event.startDate}<AccessTimeIcon sx={{ height: "0.5em", width: '0.5em', ml: '0.5em' }} /> {event.startTime} - {event.endTime}</Typography>

                    <Typography variant="subtitle2">Recurrence:</Typography>
                    <Typography>{mapEnumToString(event.recurrenceType)}</Typography>

                    <Typography variant="subtitle2">Tags:</Typography>
                    <EventTagList tags={event.eventTags} />

                    <Typography variant="subtitle2">Owner:</Typography>
                    <UserView user={event.owner} />

                </Grid>

                <Grid item xs={12} sm={5}>
                    <Typography variant="h6" gutterBottom>
                        Members:
                    </Typography>
                    <UserViewList users={event.members} />
                </Grid>

            </Grid>
        </Box>
    )
}
