import { Link } from 'react-router-dom'
import { Box } from '@mui/material'
import EventTagList from '../EventTagList/EventTagList'
import routes from '../../constants/route-constants'

export default function EventView ({ event }) {

    const eventViewSX = {
        boxShadow: 1,
        borderRadius: 3,
        p: 1,
        width: 300,
        height: 120
    }

    const descriptionSX = {
        lineHeight: '1.5em',
        height: '3em',
        overflow: 'hidden'
    }

    const titleSX = { fontWeight: 'bold' }

    return (
        <Box sx={eventViewSX}>
            <Box sx={titleSX}>{event.title}</Box>
            <Box sx={descriptionSX}>{event.description}</Box>
            <EventTagList tags={event.eventTags} />
            <Link to={`/${routes.events}/${event.id}`}>To event details</Link>
        </Box>
    )
}