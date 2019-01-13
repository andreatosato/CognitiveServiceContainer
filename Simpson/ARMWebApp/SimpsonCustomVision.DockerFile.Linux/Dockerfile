FROM python:3.5

ADD app /app

RUN pip install --upgrade pip
RUN pip install https://github.com/lhelontra/tensorflow-on-arm/releases/download/v1.5.0/tensorflow-1.5.0-cp35-none-linux_armv7l.whl
RUN pip install -r /app/requirements.txt

# Expose the port
EXPOSE 80

# Set the working directory
WORKDIR /app

# Run the flask server for the endpoints
CMD python app.py
