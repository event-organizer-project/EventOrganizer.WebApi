import { Button } from '@mui/material';
import { Link } from 'react-router-dom';
import routes from '../../constants/route-constants'

export default function OwnEventsPage () {
    return (
    <div>
        <h3>There will be a page with own created and joined events.</h3>
        <Button component={Link} to={`${routes.events}/0`} variant="contained" color="primary">
            Create New Event
        </Button>
    </div>
    )
}