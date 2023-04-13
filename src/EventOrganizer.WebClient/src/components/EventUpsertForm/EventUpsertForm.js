import { useForm, Controller } from 'react-hook-form'
import { FormGroup, TextField, Button, FormHelperText } from '@mui/material';
import { DatePicker, MobileTimePicker } from '@mui/x-date-pickers'
import { MuiChipsInput } from 'mui-chips-input'
import dayjs from 'dayjs'
import dateTimeSetting from '../../constants/date-time-setting'

export default function EventUpsertForm({ event, submitAction, cancelAction }) {
    const {
        control,
        watch,
        formState: {
            errors,
            isValid
        },
        handleSubmit
    } = useForm({
        mode: 'onBlur',
        defaultValues: {
            ...event,
            startDate: event.startDate && dayjs(event.startDate),
            startTime: event.startTime && dayjs(event.startTime, dateTimeSetting.timeFormat),
            endTime: event.endTime && dayjs(event.endTime, dateTimeSetting.timeFormat)
        }
    });

    const onSubmit = (data) => {
        var result = {
            ...data,
            startDate: data.startDate.format(dateTimeSetting.dateFormat),
            startTime: data.startTime.format(dateTimeSetting.timeFormat),
            endTime: data.endTime.format(dateTimeSetting.timeFormat)
        }
        submitAction(result);
    }

    const inputStyles = {
        marginBottom: '2vh',
        width: '100%'
    }

    return (
        <div>
            <form onSubmit={handleSubmit(onSubmit)}>
                <Controller name='title'
                    control={control}
                    rules={{
                        required: 'Title is required',
                        minLength: {
                            value: 5,
                            message: 'Title must be longer than 5 characters'
                        },
                        maxLength: {
                            value: 50,
                            message: 'Title must be shorter than 50 characters'
                        },
                    }}
                    render={({ field }) => (
                        <TextField {...field}
                            sx={inputStyles}
                            size='small'
                            required
                            label='Title'
                            variant='outlined'
                            error={!!errors.title}
                            helperText={errors.title && errors.title?.message} />
                    )} />
                <FormGroup />

                <Controller name='description'
                    control={control}
                    rules={{
                        required: 'Description is required',
                        minLength: {
                            value: 10,
                            message: 'Description must be longer than 10 characters'
                        },
                        maxLength: {
                            value: 1000,
                            message: 'Description must be shorter than 1000 characters'
                        },
                    }}
                    render={({ field }) => (
                        <TextField {...field}
                            sx={inputStyles}
                            size='small'
                            required
                            multiline
                            rows={4}
                            label='Description'
                            variant='outlined'
                            error={!!errors.description}
                            helperText={errors.description && errors.description?.message} />
                    )} />

                <Controller name='startDate'
                    control={control}
                    rules={{
                        required: 'Start date is required',
                    }}
                    render={({ field }) => (
                        <DatePicker {...field}
                            sx={inputStyles}
                            componentsProps={{
                                textField: {
                                    size: 'small',
                                    required: true,
                                    label: 'Start Date',
                                    variant: 'outlined',
                                    error: !!errors.startDate
                                }
                            }}
                        />
                    )} />
                <FormHelperText error sx={{ ml: '14px' }}>{errors.startDate && errors.startDate?.message}</FormHelperText>
                <FormGroup sx={{ flexDirection: 'row', justifyContent: 'space-between', marginBottom: '2vh' }}>
                    <Controller name='startTime'
                        control={control}
                        rules={{
                            required: 'Start time is required',
                        }}
                        render={({ field }) => (
                            <MobileTimePicker {...field}
                                sx={{ width: '45%' }}
                                componentsProps={{
                                    textField: {
                                        size: 'small',
                                        required: true,
                                        label: 'Start Time',
                                        variant: 'outlined',
                                        error: !!errors.startTime
                                    }
                                }}
                            />
                        )} />
                    <Controller name='endTime'
                        control={control}
                        rules={{
                            required: 'End time is required',
                        }}
                        render={({ field }) => (
                            <MobileTimePicker {...field}
                                sx={{ width: '45%' }}
                                componentsProps={{
                                    textField: {
                                        size: 'small',
                                        required: true,
                                        label: 'End Time',
                                        variant: 'outlined',
                                        error: !!errors.endTime
                                    }
                                }}
                            />
                        )} />
                    {(!!errors.startTime || !!errors.endTime) &&
                        <FormHelperText error sx={{ ml: '14px' }}>Error with event time</FormHelperText>
                    }
                </FormGroup>
                <Controller name='eventTags'
                    control={control}
                    render={({ field }) => (
                        <MuiChipsInput {...field}
                            sx={inputStyles}
                            label='Event Tags'
                            variant='outlined'
                            size='small'
                        />
                    )} />
                <FormGroup sx={{ flexDirection: 'row', justifyContent: 'space-between', marginBottom: '2vh' }}>
                    <Button variant='contained' color='primary' onClick={cancelAction}>Cancel</Button>
                    <Button type='submit' variant='contained' color='success'>Submit</Button>
                </FormGroup>
            </form>
        </div>
    )
}