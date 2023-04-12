import { useState } from 'react';
import { useSelector } from 'react-redux'
import { useHistory } from "react-router-dom";
import { styled, alpha } from '@mui/material/styles';
import Menu from '@mui/material/Menu';
import MenuItem from '@mui/material/MenuItem';
import EditIcon from '@mui/icons-material/Edit';
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';
import Tooltip from '@mui/material/Tooltip';
import BuildIcon from '@mui/icons-material/Build';
import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import BlockIcon from '@mui/icons-material/Block';
import eventRequestService from 'services/eventRequestService'
import routes from 'constants/route-constants'

export default function EventOptionsMenu({ event, setEvent, toUpdateEvent }) {

    const history = useHistory();
    const [anchorEl, setAnchorEl] = useState(null);
    const user = useSelector(state => state.auth.user);

    const isOwnerOptions = user.id == event.owner.id;
    const isEventScheduled = !isOwnerOptions && event.members.some(x => x.id == user.id);

    const handleClick = (e) => setAnchorEl(e.currentTarget);

    const handleClose = () => setAnchorEl(null);

    const displayMenuItemStyle = (display) => display ? '' : 'none'


    const scheduleEvent = () => {
        handleClose();
        eventRequestService.schedule(event.id, true)
            .then(result => {
                setEvent(result);
            })
    }

    const unscheduleEvent = () => {
        handleClose();
        eventRequestService.schedule(event.id, false)
            .then(result => {
                history.push(routes.events);
            })
    }

    const deleteEvent = () => {
        handleClose();
        eventRequestService.delete(event.id)
            .then(result => {
                history.push(routes.events);
            })
    }

    return (
        <div>
            <Tooltip onClick={handleClick}>
                <IconButton>
                    <BuildIcon />
                </IconButton>
            </Tooltip>
            <StyledMenu
                id="demo-customized-menu"
                MenuListProps={{
                    'aria-labelledby': 'demo-customized-button',
                }}
                anchorEl={anchorEl}
                open={Boolean(anchorEl)}
                onClose={handleClose}
            >
                <MenuItem sx={{ display: displayMenuItemStyle(isOwnerOptions) }}
                    onClick={toUpdateEvent}>
                    <EditIcon />
                    Edit
                </MenuItem>
                <MenuItem sx={{ display: displayMenuItemStyle(isOwnerOptions) }}
                    onClick={deleteEvent}>
                    <DeleteIcon />
                    Delete
                </MenuItem>

                <MenuItem sx={{ display: displayMenuItemStyle(!isOwnerOptions && !isEventScheduled) }}
                    onClick={scheduleEvent}>
                    <AddCircleOutlineIcon />
                    Schedule
                </MenuItem>
                <MenuItem sx={{ display: displayMenuItemStyle(!isOwnerOptions && isEventScheduled) }}
                    onClick={unscheduleEvent}>
                    <BlockIcon />
                    Unschedule
                </MenuItem>
            </StyledMenu>
        </div>
    );
}

const StyledMenu = styled((props) => (
    <Menu
        elevation={0}
        anchorOrigin={{
            vertical: 'bottom',
            horizontal: 'right',
        }}
        transformOrigin={{
            vertical: 'top',
            horizontal: 'right',
        }}
        {...props}
    />
))(({ theme }) => ({
    '& .MuiPaper-root': {
        borderRadius: 6,
        marginTop: theme.spacing(1),
        minWidth: 180,
        color:
            theme.palette.mode === 'light' ? 'rgb(55, 65, 81)' : theme.palette.grey[300],
        boxShadow:
            'rgb(255, 255, 255) 0px 0px 0px 0px, rgba(0, 0, 0, 0.05) 0px 0px 0px 1px, rgba(0, 0, 0, 0.1) 0px 10px 15px -3px, rgba(0, 0, 0, 0.05) 0px 4px 6px -2px',
        '& .MuiMenu-list': {
            padding: '4px 0',
        },
        '& .MuiMenuItem-root': {
            '& .MuiSvgIcon-root': {
                fontSize: 18,
                color: theme.palette.text.secondary,
                marginRight: theme.spacing(1.5),
            },
            '&:active': {
                backgroundColor: alpha(
                    theme.palette.primary.main,
                    theme.palette.action.selectedOpacity,
                ),
            },
        },
    },
}));
